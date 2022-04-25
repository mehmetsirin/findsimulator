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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public  class SessionDetailManager:  ISessionDetailManager
    {
        private readonly ISessionDetailRepository sessionDetail;
        private readonly IBaseManager<int> baseManager;
         private  readonly IMapper mapper;
        private readonly ISimulatorDeviceService simulatorDeviceService;
        private readonly ISessionsManager sessionsManager;
        public SessionDetailManager(ISessionDetailRepository sessionDetail, IBaseManager<int> baseManager, IMapper mapper, ISimulatorDeviceService simulatorDeviceService, ISessionsManager sessionsManager)
        {
            this.sessionDetail = sessionDetail;
            this.baseManager = baseManager;
            this.mapper = mapper;
            this.simulatorDeviceService = simulatorDeviceService;
            this.sessionsManager = sessionsManager;
        }

        public    async Task<DataResult<List<CalendarView>>> GetCalendarAsync()
        {
            var sessionsPerson =   await baseManager.ListAsync<SessionPerson>();
            var details = sessionDetail.List<SessionDetails>().GetAwaiter().GetResult().Data;
            var sesions = await baseManager.ListAsync<Sessions>();
            var resData = new List<CalendarView>();
            foreach (var item in sessionsPerson.Data)
            {
                var res = new CalendarView();
                var detail = details.Where(y => y.ID == item.SessionDetailID).FirstOrDefault();
                if(detail is not null)
                {
                    var session = sesions.Data.Where(y => y.ID == detail.SessionsID).FirstOrDefault();
                    res.End = detail.EndDate;
                    res.Id = item.ID;
                    res.Start = detail.StartDate;
                    res.Title = item.FirstName??"İsim Girilmemiş";
                    res.Url = "";
                   
                    res.ExtendedProps = new ExtendedProps(session.AircraftType);
                    resData.Add(res);

                }

            }
            return new DataResult<List<CalendarView>>(ResultStatus.Success,resData);
        }

        public    async Task<DataResult<Tuple<SimulatorDevice, List<SessionDetails>>>> GetSessionDetail(int sessionID, int SimulatorDeviceID)
        {
            var device = await baseManager.GetByIDAsync<SimulatorDevice>(SimulatorDeviceID);
            var sessionsDetailData = sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => y.SessionsID == sessionID).ToList();

            var tuple = new Tuple<SimulatorDevice, List<SessionDetails>>(device.Data, sessionsDetailData);
            return new DataResult<Tuple<SimulatorDevice, List<SessionDetails>>>(ResultStatus.Success,tuple);
        }

        public async  Task<DataResult<List<SessionDetails>>> GetSessionDetail(List<int> sessionsIds)
        {
            var sessionsDetailData =   sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => sessionsIds.Contains(y.SessionsID)).ToList();
            return new DataResult<List<SessionDetails>>(ResultStatus.Success,sessionsDetailData);
        }

        public   async Task<DataResult<bool>> RemoveAsync(int id)
        {
            var data =   await sessionDetail.DeleteAsync<SessionDetails>(id);
            return new DataResult<bool>(ResultStatus.Success, true);
        }

        public  async Task<DataResult<bool>> SessionAddAsync(SessionCreate model)
        {
            var simulalator =   simulatorDeviceService.GetByIDAsync(model.SimulatorDeviceID).GetAwaiter().GetResult().Data;
            var sessions = new Sessions(model.StartDate,model.EndDate,"",true,simulalator.CompanyName,simulalator.SimulatorTypeName,simulalator.CraftName,model.Engine,model.Price,model.SimulatorDeviceID);
            await  sessionsManager.AddAsync(sessions);
            return new DataResult<bool>(ResultStatus.Success,true);


        }

        public  async Task<DataResult<bool>> SessionDetailAddAsync(SessionDetailCreate models)
        {

            var sessionDetails = mapper.Map<List<SessionDetails>>(models.sessionDates);
            sessionDetails.ForEach(y => { y.SessionsID = models.SessionsID;   y.EndDate = y.EndDate.AddHours(3);y.StartDate = y.StartDate.AddHours(3); });
             await sessionDetail.AddManyAsync<SessionDetails>(sessionDetails);
             await  sessionDetail.SaveChangesAsync();
            return new DataResult<bool>(ResultStatus.Success,true);
        }

        public async Task<DataResult<List<SessionwithSessionDetailView>>> SessionwithSessionDetailAsync()
        {

            var sessionDetailList = await sessionDetail.List<SessionDetails>();
            var resData = mapper.Map<List<SessionwithSessionDetailView>>(sessionDetailList.Data);
            return new DataResult<List<SessionwithSessionDetailView>>(ResultStatus.Success, resData);

        }
       

    }
}
