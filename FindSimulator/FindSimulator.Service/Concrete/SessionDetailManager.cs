using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Repositories.EntityRepository;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Session;
using FindSimulator.Service.Model.SessionDetail;
using FindSimulator.Service.Model.SimulatorDevice;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using Fleet.Share.ComplexTypes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using FindSimulator.Service.Model.Calendar;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Service.Model.SessionPerson;
using FindSimulator.Service.Model.Users;
using FindSimulator.Service.Model.RequestModel;
using static Fleet.Share.ComplexTypes.CommonEnum;

namespace FindSimulator.Service.Concrete
{
    public  class SessionDetailManager:  ISessionDetailManager
    {
        private readonly ISessionDetailRepository _sessionDetail;
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IBaseManager<int> baseManager;
         private  readonly IMapper mapper;
        private readonly ISimulatorDeviceService simulatorDeviceService;
        private readonly ISessionsManager sessionsManager;
        public Infrastructure.Repositories.BaseRepository.IBaseRepository<int> baseRepository;

        public SessionDetailManager(ISessionDetailRepository sessionDetail, IBaseManager<int> baseManager, IMapper mapper, ISimulatorDeviceService simulatorDeviceService, ISessionsManager sessionsManager, ISessionsRepository sessionsRepository, IBaseRepository<int> _baseRepository)
        {
            this._sessionDetail = sessionDetail;
            this.baseManager = baseManager;
            this.mapper = mapper;
            this.simulatorDeviceService = simulatorDeviceService;
            this.sessionsManager = sessionsManager;
            this._sessionsRepository = sessionsRepository;
            this.baseRepository = _baseRepository;

        }

        public    async Task<DataResult<List<CalendarView>>> GetCalendarAsync()
        {
            var sessionsPerson =   await baseManager.ListAsync<SessionPerson>();
            var details = _sessionDetail.List<SessionDetails>().GetAwaiter().GetResult().Data;
            var sessions = await baseManager.ListAsync<Sessions>();
            var resData = new List<CalendarView>();
            foreach (var item in sessionsPerson.Data)
            {
                var res = new CalendarView();
                var detail = details.Where(y => y.ID == item.SessionDetailID).FirstOrDefault();
                if(detail is not null)
                {
                    var session = sessions.Data.Where(y => y.ID == detail.SessionsID).FirstOrDefault();
                    res.End = detail.EndDate;
                    res.Id = item.ID;
                    res.Start = detail.StartDate;
                    res.Title = item.FirstName??"İsim Girilmemiş";
                    res.Title = item.FirstName + " " + detail.StartDate.Hour + ":" + detail.StartDate.Minute + "-" + detail.EndDate.Hour + ":" + detail.EndDate.Hour;

                    res.Url = "";
                    res.Reserved = detail.Reserved;
                    res.ExtendedProps = new ExtendedProps(detail.Reserved==1? "Reserved": "NotReserved", session.AircraftType,item.SessionID);
                    resData.Add(res);

                }

            }
            return new DataResult<List<CalendarView>>(ResultStatus.Success,resData);
        }

        public  async Task<DataResult<List<CalendarView>>> GetCalendarAsync(int simulatorDeviceID, string aircraftType)
        {

            var sessions = _sessionsRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => y.SimulatorDeviceID==simulatorDeviceID && y.AircraftType==aircraftType).ToList();
            var sessionDetailList = _sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => sessions.Select(y=>y.ID).ToList().Contains(y.SessionsID)).ToList();


            if (sessionDetailList.Count == 0)
                return new DataResult<List<CalendarView>>(ResultStatus.DataNull);
            var resData = new List<CalendarView>();
            foreach (var item in sessionDetailList.OrderBy(Y=>Y.StartDate).ToList())
            {
                var res = new CalendarView();
                res.OrderID = item.OrderID;
                    res.End = item.EndDate;
                    res.Id = item.ID;
                    res.Start = item.StartDate;
                    res.Title = "Title" ?? "İsim Girilmemiş";
                    res.Title = item.StartDate.ToString("hh") + ":" + item.StartDate.ToString("mm") + "-" + item.EndDate.ToString("hh") + ":" + item.EndDate.ToString("mm");
                    res.Url = "";
                    res.Reserved = item.Reserved;
                    res.Status = item.Status;
                    res.ExtendedProps = new ExtendedProps(Enum.GetName(typeof(CommonEnum.SessionDetailStatus),item.Status).ToString(), "",item.SessionsID);
                    resData.Add(res);

            }
            return new DataResult<List<CalendarView>>(ResultStatus.Success, resData);

        }

        public    async Task<DataResult<Tuple<SimulatorDevice, List<SessionDetails>>>> GetSessionDetail(int sessionID, int SimulatorDeviceID)
        {
            var device = await baseManager.GetByIDAsync<SimulatorDevice>(SimulatorDeviceID);
            var sessionsDetailData = _sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => y.SessionsID == sessionID).ToList();

            var tuple = new Tuple<SimulatorDevice, List<SessionDetails>>(device.Data, sessionsDetailData);
            return new DataResult<Tuple<SimulatorDevice, List<SessionDetails>>>(ResultStatus.Success,tuple);
        }

        public async  Task<DataResult<List<SessionDetails>>> GetSessionDetail(List<int> sessionsIds)
        {
            var sessionsDetailData =   _sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => sessionsIds.Contains(y.SessionsID)).ToList();
            return new DataResult<List<SessionDetails>>(ResultStatus.Success,sessionsDetailData);
        }

        public   async Task<DataResult<bool>> RemoveAsync(int id)
        {
            var data =   await _sessionDetail.DeleteAsync<SessionDetails>(id);
            return new DataResult<bool>(ResultStatus.Success, true);
        }

        public  async Task<DataResult<bool>> SessionAddAsync(SessionCreate model,int companyID,int userId)
        {

            var slotDate = JsonSerializer.Serialize(model.SlotDate.Select(y => new { startDate = y.StartDate.ToString("HH:mm"), endDate = y.EndDate.ToString("HH:mm") }));
            var simulalator =   simulatorDeviceService.GetByIDAsync(model.SimulatorDeviceID).GetAwaiter().GetResult().Data;
            var sessions = new Sessions(model.StartDate, model.EndDate, "", true, companyID, simulalator.SimulatorTypeName, simulalator.CraftName, model.Engine, model.Price, model.SimulatorDeviceID, model.Currency, slotDate) ;
          var session=    await  sessionsManager.AddAsync(sessions);
              
            List<SessionDetails> sessionDetails = new List<SessionDetails>();

            for ( var day=model.StartDate;  model.StartDate<model.EndDate;  model.StartDate=model.StartDate.AddDays(1) )
            {

                for (int i = 0; i < model.SlotDate.Count; i++)
                {
                    sessionDetails.Add(  new SessionDetails() {OrderID=Guid.NewGuid(), Price=model.Price,SessionsID=session.Data.ID, StartDate= new DateTime(model.StartDate.Year,model.StartDate.Month,model.StartDate.Day,model.SlotDate[i].StartDate.Hour,model.SlotDate[i].StartDate.Minute,0), EndDate= new DateTime(model.StartDate.Year,model.StartDate.Month, model.StartDate.Day, model.SlotDate[i].EndDate.Hour, model.SlotDate[i].EndDate.Minute, 0) });
                }
            }
            await  _sessionDetail.AddManyAsync<SessionDetails>(sessionDetails);

                _sessionDetail.SaveChanges();

            return new DataResult<bool>(ResultStatus.Success,true);

        }
        public  async Task<DataResult<bool>> SessionDetailAddAsync(SessionDetailCreate models)
        {

            var sessionDetails = mapper.Map<List<SessionDetails>>(models.sessionDates);
            sessionDetails.ForEach(y => { y.SessionsID = models.SessionsID;   y.EndDate = y.EndDate.AddHours(3);y.StartDate = y.StartDate.AddHours(3); });
             await _sessionDetail.AddManyAsync<SessionDetails>(sessionDetails);
             await _sessionDetail.SaveChangesAsync();
            return new DataResult<bool>(ResultStatus.Success,true);
        }

       public async  Task<DataResult<bool>> SessionStateUpdateAsync(List<SessionDetailStateUpdate> sessionDetailStateUpdates)
        {

            var sessionDetailList = _sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => sessionDetailStateUpdates.Select(y => y.SessionDetailID).ToList().Contains(y.ID)).ToList();

            foreach (var item in sessionDetailList)
            {
                item.Status = (int)CommonEnum.SessionDetailStatus.Open;
                _sessionDetail.UpdateOne<Domain.Entities.SessionDetails>(item);
            }
            _sessionDetail.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success,true);
             
        }

        public Task<DataResult<bool>> SessionRemove(int id)
        {
            //var  data = var sessionsPerson = await baseManager.<SessionPerson>();
            return null;
        }

        public async Task<DataResult<List<SessionDetailView>>> SessionwithSessionDetailAsync()
        {

            var sessionDetailList = await _sessionDetail.List<SessionDetails>();
            var resData = mapper.Map<List<SessionDetailView>>(sessionDetailList.Data);
            return new DataResult<List<SessionDetailView>>(ResultStatus.Success, resData);

        }
        public async Task<DataResult<List<SessionDetailWithSessionView>>> GetSessionDetailOrderAsync()
        {

            var sessionsPersons = baseRepository.GetQueryable<SessionPerson>().GetAwaiter().GetResult().Data.ToList();
            var sessions = baseRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => sessionsPersons.Select(x => x.SessionID).ToList().Contains(y.ID)).ToList();
            var sessionsView = mapper.Map<List<SessionsView>>(sessions);
            var personView = mapper.Map<List<SessionPersonView>>(sessionsPersons);
            var sessionDetails = baseRepository.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => sessionsPersons.Select(x => x.SessionDetailID).ToList().Contains(y.ID)).ToList();
            var resDatas = new List<SessionDetailWithSessionView>();
            foreach (var item in sessionDetails)
            {
                var resData = new SessionDetailWithSessionView();
                resData.OrderID = item.OrderID;
                resData.EndDate = item.EndDate;
                resData.StartDate = item.StartDate;
                resData.SessionsID = item.SessionsID;
                resData.Status = item.Status;
                resData.DsStatus = (Enum.GetName(typeof(CommonEnum.SessionDetailStatus), item.Status).ToString());
                resData.SessionsView = sessionsView.Where(y => y.ID == item.SessionsID).FirstOrDefault();
                resDatas.Add(resData);

            }


            return new DataResult<List<SessionDetailWithSessionView>>(ResultStatus.Success, resDatas);
        }

        public  async Task<DataResult<bool>> OrderConfirm(OrderConfirmRequest request)
        {
            var sessionDetail = _sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y =>y.OrderID==request.OrderID).FirstOrDefault();
            sessionDetail.UpdateDate = DateTime.Now;
            sessionDetail.Status = request.Status;
             _sessionDetail.UpdateOne<SessionDetails>(sessionDetail);
               await _sessionDetail.SaveChangesAsync();
            return    new DataResult<bool>(ResultStatus.Success,true);
        }

        public  async Task<DataResult<bool>> SessionPersonWithSessionDetailUpdateAsync(SessionPersonDelete sessionPersonDelete)
        {

            var sessionDetails = _sessionDetail.GetQueryable<SessionPerson>().GetAwaiter().GetResult().Data.Where(y => y.SessionDetailID==sessionPersonDelete.SessionDetailID&&y.SessionID==sessionPersonDelete.SessionID).ToList();
            sessionDetails.ForEach(item=> {

                item.UpdateDate = DateTime.Now;
                item.IsActive = false;
            });
              await _sessionDetail.UpdateManyAsync<SessionPerson>(sessionDetails);

            var sessionDetail = _sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => y.ID==sessionPersonDelete.SessionDetailID).FirstOrDefault();
            if (sessionDetail == null)
                return new DataResult<bool>(ResultStatus.DataNull, "Böyle bir Slot bulunamadı");
            sessionDetail.Status = 1;
            sessionDetail.IsActive = true;
            sessionDetail.UpdateDate = DateTime.Now;
             await _sessionDetail.UpdateOneAsync<SessionDetails>(sessionDetail);
          await  _sessionDetail.SaveChangesAsync();
            return new DataResult<bool>(ResultStatus.Success,true);
        }
    }
}
