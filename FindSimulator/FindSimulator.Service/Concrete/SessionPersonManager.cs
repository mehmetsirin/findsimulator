using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.SessionDetail;
using FindSimulator.Service.Model.SessionPerson;
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
    public sealed partial class SessionPersonManager : ISessionPersonManager
    {

        public Infrastructure.Repositories.BaseRepository.IBaseRepository<int> baseRepository;
        private readonly IMapper mapper;
        private ISessionDetailManager sessionDetailManager;
        private ISessionsManager sessionsManager;

        public SessionPersonManager(IBaseRepository<int> baseRepository, IMapper mapper, ISessionDetailManager sessionDetailManager, ISessionsManager sessionsManager)
        {
            this.baseRepository = baseRepository;
            this.mapper = mapper;
            this.sessionsManager = sessionsManager;
            this.sessionDetailManager = sessionDetailManager;
        }

        public async Task<DataResult<bool>> AddMultipleAsync(List<SessionPersonAdd> add, int userID)
        {
            var data = mapper.Map<List<SessionPerson>>(add);
            data.ForEach(y => { y.UserID = userID; });
            await baseRepository.AddManyAsync<SessionPerson>(data);
            baseRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success);
        }

        public async Task<DataResult<SessionPersonView>> GetByIDAsync(int id)
        {
            var data = await baseRepository.GetByIdAsync<SessionPerson>(id);
            var resDataa = mapper.Map<SessionPersonView>(data.Data);
            return new DataResult<SessionPersonView>(ResultStatus.Success, resDataa);
        }

        public async Task<DataResult<List<SessionPersonView>>> ListAsync()
        {
            var data = await baseRepository.List<SessionPerson>();
            var resDataa = mapper.Map<List<SessionPersonView>>(data);
            return new DataResult<List<SessionPersonView>>(ResultStatus.Success, resDataa);
        }

        public async Task<DataResult<List<SessionPersonView>>> ListAsync(int SessionID, int SessionDetailID)
        {
            var data = baseRepository.GetQueryable<SessionPerson>().GetAwaiter().GetResult().Data.Where(y => y.SessionID == SessionID && y.SessionDetailID == SessionDetailID).ToList();
            var resDataa = mapper.Map<List<SessionPersonView>>(data);
            return new DataResult<List<SessionPersonView>>(ResultStatus.Success, resDataa);
        }

        public async Task<Result> Remove(int ID)
        {
            var data = baseRepository.GetById<SessionPerson>(ID);
            data.Data.IsActive = false;
            baseRepository.UpdateOne<SessionPerson>(data.Data);
            await baseRepository.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<Result> UpdateAsync(SessionPersonUpdate update)
        {
            var data = baseRepository.GetById<SessionPerson>(update.ID).Data;
            data.FirstName = update.FirstName;
            data.LastName = update.LastName;
            data.FirstName = update.LicenseNumber;
            data.LicenseNumber = update.Nationality;
            data.Nationality = update.TelNo;
            baseRepository.UpdateOne<SessionPerson>(data);
            await baseRepository.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<DataResult<List<SessionPersonView>>> ListAsync(int SessionID)
        {
            var data = baseRepository.GetQueryable<SessionPerson>().GetAwaiter().GetResult().Data.Where(y => y.SessionID == SessionID).ToList();
            var resDataa = mapper.Map<List<SessionPersonView>>(data);
            return new DataResult<List<SessionPersonView>>(ResultStatus.Success, resDataa);
        }

        public async Task<DataResult<bool>> AddAsync(SessionPersonAdd add)
        {
            var data = mapper.Map<SessionPerson>(add);
            baseRepository.AddOne<SessionPerson>(data);
            baseRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success);
        }

        public async Task<DataResult<List<SessionwithPersonwithDetailModel>>> GetUserByIDSessions(int userID)
        {

            var resData = new List<SessionwithPersonwithDetailModel>();

            var sessionsPersons = baseRepository.GetQueryable<SessionPerson>().GetAwaiter().GetResult().Data.Where(y => y.UserID == userID).ToList();
            var sessions = await sessionsManager.ListAsync(sessionsPersons.Select(y => y.SessionID).ToList());
            var SessionDetails = await sessionDetailManager.GetSessionDetail(sessions.Data.Select(y => y.ID).ToList());

            foreach (var item in sessions.Data)
            {
                var itemdata = new SessionwithPersonwithDetailModel();
                itemdata.AircraftType = item.AircraftType;
                itemdata.EndDate = SessionDetails.Data.Where(y => y.SessionsID == item.ID).FirstOrDefault().EndDate;
                itemdata.Engine = item.Engine;
                itemdata.IsTeacher = item.IsTeacher;
                itemdata.Location = item.Location;
                itemdata.SimulatorType = item.SimulatorType;
                itemdata.StartDate = SessionDetails.Data.Where(y => y.SessionsID == item.ID).FirstOrDefault().StartDate;


                var personView = mapper.Map<List<SessionPersonView>>(sessionsPersons.Where(y => y.SessionID == item.ID).ToList());
                itemdata.sessionPersonViews.AddRange(personView);
                resData.Add(itemdata);

            }
            return new DataResult<List<SessionwithPersonwithDetailModel>>(ResultStatus.Success, resData);
        }

      
    }
}
