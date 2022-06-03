﻿using AutoMapper;

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
        private readonly ISessionDetailRepository _sessionDetail;
        private readonly IBaseManager<int> baseManager;
         private  readonly IMapper mapper;
        private readonly ISimulatorDeviceService simulatorDeviceService;
        private readonly ISessionsManager sessionsManager;
        public SessionDetailManager(ISessionDetailRepository sessionDetail, IBaseManager<int> baseManager, IMapper mapper, ISimulatorDeviceService simulatorDeviceService, ISessionsManager sessionsManager)
        {
            this._sessionDetail = sessionDetail;
            this.baseManager = baseManager;
            this.mapper = mapper;
            this.simulatorDeviceService = simulatorDeviceService;
            this.sessionsManager = sessionsManager;
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
                    res.ExtendedProps = new ExtendedProps(detail.Reserved==1? "Reserved": "NotReserved", session.AircraftType);
                    resData.Add(res);

                }

            }
            return new DataResult<List<CalendarView>>(ResultStatus.Success,resData);
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
            var simulalator =   simulatorDeviceService.GetByIDAsync(model.SimulatorDeviceID).GetAwaiter().GetResult().Data;
            var sessions = new Sessions(model.StartDate,model.EndDate,"",true,companyID ,simulalator.SimulatorTypeName,simulalator.CraftName,model.Engine,model.Price,model.SimulatorDeviceID,model.Currency);
          var session=    await  sessionsManager.AddAsync(sessions);
              
            List<SessionDetails> sessionDetails = new List<SessionDetails>();

            for ( var day=model.StartDate;  model.StartDate<model.EndDate;  model.StartDate=model.StartDate.AddDays(1) )
            {

                for (int i = 0; i < model.SlotDate.Count; i++)
                {
                    sessionDetails.Add(  new SessionDetails() { Price=model.Price,SessionsID=session.Data.ID, StartDate= new DateTime(model.StartDate.Year,model.StartDate.Month,model.StartDate.Day,model.SlotDate[i].StartDate.Hour,model.SlotDate[i].StartDate.Minute,0), EndDate= new DateTime(model.StartDate.Year,model.StartDate.Month, model.StartDate.Day, model.SlotDate[i].EndDate.Hour, model.SlotDate[i].EndDate.Minute, 0) });
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

        public Task<DataResult<bool>> SessionRemove(int id)
        {
            //var  data = var sessionsPerson = await baseManager.<SessionPerson>();
            return null;
        }

        public async Task<DataResult<List<SessionwithSessionDetailView>>> SessionwithSessionDetailAsync()
        {

            var sessionDetailList = await _sessionDetail.List<SessionDetails>();
            var resData = mapper.Map<List<SessionwithSessionDetailView>>(sessionDetailList.Data);
            return new DataResult<List<SessionwithSessionDetailView>>(ResultStatus.Success, resData);

        }
       

    }
}
