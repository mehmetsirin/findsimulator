using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Session;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using Fleet.Share.ComplexTypes;

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
        readonly ISessionsRepository _sessionsRepository;

        public SessionsManager(IMapper mapper, ISessionsRepository sessions)
        {
            this.mapper = mapper;
            this._sessionsRepository = sessions;
        }

        public async Task<DataResult<Sessions>> AddAsync(Sessions model)
        {
            await _sessionsRepository.AddOneAsync<Sessions>(model);
            _sessionsRepository.SaveChanges();
            return new DataResult<Sessions>(ResultStatus.Success, model);
        }



        public async Task<DataResult<List<SessionsView>>> ListAsync(List<int> Ids)
        {

            var sessionData = _sessionsRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => Ids.Contains(y.ID)).ToList();
            var resData = mapper.Map<List<SessionsView>>(sessionData);

            return new DataResult<List<SessionsView>>(ResultStatus.Success, resData);

        }
        public async Task<DataResult<List<SessionsView>>> ListAsync()
        {

            var sessionData = _sessionsRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.ToList();
            var resData = mapper.Map<List<SessionsView>>(sessionData);

            return new DataResult<List<SessionsView>>(ResultStatus.Success, resData);

        }

        public async Task<DataResult<bool>> RemoveAsync(int id)
        {
            var sessionDetails = _sessionsRepository.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => y.SessionsID == id).ToList();
            sessionDetails = sessionDetails.Where(y => y.Status == (int)CommonEnum.SessionDetails.Reserved).ToList();
            if (sessionDetails.Count() > 0)
                return new DataResult<bool>(ResultStatus.Info, "Daha Onceden Satılan Slot Olduğundan Silinemez");
             sessionDetails.ForEach(y => { y.UpdateDate = DateTime.Now; y.IsActive = false; });
            var  session = _sessionsRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y =>y.ID==id).FirstOrDefault();
            session.IsActive = false;
            _sessionsRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success,true);
        }

        public async Task<DataResult<List<SessionsView>>> Search(SimulatorSearcModel dto)
        {

            List<Sessions> sessionsList = null;
            if (DateTime.Now > dto.StarDate)
                sessionsList = _sessionsRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => y.StartDate > dto.StarDate && y.EndDate < dto.EndDate).ToList();
            else
                sessionsList = _sessionsRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => y.StartDate > dto.StarDate && y.EndDate < dto.EndDate).ToList();
            if (!string.IsNullOrEmpty(dto.simulatorType))
                sessionsList = sessionsList.Where(y => y.SimulatorType == dto.simulatorType).ToList();
            if (!string.IsNullOrEmpty(dto.aircraftType))
                sessionsList = sessionsList.Where(y => y.AircraftType == dto.aircraftType).ToList();
            if (!string.IsNullOrEmpty(dto.location))
                sessionsList = sessionsList.Where(y => y.Location == dto.location).ToList();

            var data = mapper.Map<List<SessionsView>>(sessionsList);
            return new DataResult<List<SessionsView>>(ResultStatus.Success, data);

        }

        public async Task<DataResult<SessionsView>> SimulatorSessionByID(int SessionID)
        {

            var session = _sessionsRepository.GetQueryable<Sessions>().GetAwaiter().GetResult().Data.Where(y => y.ID == SessionID).FirstOrDefault();

            var data = mapper.Map<SessionsView>(session);
            return new DataResult<SessionsView>(ResultStatus.Success, data);
        }
        public async Task<DataResult<Sessions>> UpdateAsync(SessionUpdate _session)
        {
            var session =  SimulatorSessionByID(_session.ID).GetAwaiter().GetResult().Data;
            var slotList = _sessionsRepository.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => y.SessionsID == _session.ID).OrderBy(y=>y.StartDate).ToList();
            var reservedSlots = slotList.Where(y => y.Status == (int)CommonEnum.SessionDetails.Reserved).ToList();
            if (reservedSlots.Count() > 0)
            {
                session.StartDate = slotList.FirstOrDefault().StartDate;
                session.EndDate = slotList.LastOrDefault().EndDate;
                slotList = slotList.Where(x => !reservedSlots.Any(y=>y.ID==x.ID)).ToList();

                slotList.ForEach(y => { y.Price = _session.Price; });
            }
            else
            {
                session.StartDate = _session.StartDate;
                session.EndDate = _session.EndDate;
                foreach (var item in slotList)
                {
                   if(item.StartDate>_session.StartDate && item.EndDate < _session.EndDate)
                    {
                        item.IsActive = false;
                        item.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        item.Price = _session.Price;
                        item.UpdateDate = DateTime.Now;
                    }
                }
            }
            _sessionsRepository.SaveChanges();
            return new DataResult<Sessions>(ResultStatus.Success);

        }
    }
}
