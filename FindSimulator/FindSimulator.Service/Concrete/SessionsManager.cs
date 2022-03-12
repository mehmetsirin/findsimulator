using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class SessionsManager : ISessionsManager
    {
         readonly IMapper mapper;
        readonly ISessionsRepository sessions;

        public SessionsManager(IMapper mapper, ISessionsRepository sessions)
        {
            this.mapper = mapper;
            this.sessions = sessions;
        }

        public   async  Task<DataResult<List<SessionsView>>>Search(SimulatorSearcModel dto)
        {

            List<Sessions> sessionsList = null;
            if (DateTime.Now>dto.StarDate)
               sessionsList = sessions.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => y.StartDate > dto.StarDate && y.EndDate < dto.EndDate).ToList();
            else
            sessionsList =  sessions.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y =>y.StartDate>dto.StarDate  && y.EndDate < dto.EndDate).ToList();
            if (!string.IsNullOrEmpty(dto.simulatorType))
                sessionsList = sessionsList.Where(y=>y.SimulatorType==dto.simulatorType).ToList();
            if (!string.IsNullOrEmpty(dto.aircraftType))
                sessionsList = sessionsList.Where(y => y.AircraftType == dto.aircraftType).ToList();
            if (!string.IsNullOrEmpty(dto.location))
                sessionsList = sessionsList.Where(y => y.Location == dto.location).ToList();

            var data = mapper.Map<List<SessionsView>>(sessionsList);
            return new DataResult<List<SessionsView>>(ResultStatus.Success,  data);

        }

        public   async Task<DataResult<SessionsView>> SimulatorSessionByID(int SessionID)
        {

            var session = sessions.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => y.ID == SessionID).FirstOrDefault();

            var data = mapper.Map<SessionsView>(session);
            return new DataResult<SessionsView>(ResultStatus.Success,data);

        }
    }
}
