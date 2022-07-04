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
            var sessionsPersonsGroup = sessionsPersons.GroupBy(y =>   new { y.SessionDetailID, y.SessionID}).ToList();

            foreach (var item in sessionsPersonsGroup)
            {
                var session = sessions.Data.Where(y => y.ID == item.Key.SessionID).FirstOrDefault();
                var sessionDetail = SessionDetails.Data.Where(y => y.ID == item.Key.SessionDetailID).FirstOrDefault();
                  
                if (session is null||  sessionDetail is null)
                    continue;
                var itemdata = new SessionwithPersonwithDetailModel();
                itemdata.AircraftType = session.AircraftType;
                itemdata.Engine = session.Engine;
                itemdata.IsTeacher = session.IsTeacher;
                itemdata.Location = session.Location;
                itemdata.SimulatorType = session.SimulatorType;
                itemdata.StartDate = sessionDetail.StartDate;
                itemdata.EndDate = sessionDetail.EndDate;
                itemdata.SessionDetailID = item.Key.SessionDetailID;
                itemdata.SessionID = item.Key.SessionID;
                itemdata.SessionDetailStatus = Enum.GetName(typeof(CommonEnum.SessionDetailStatus), sessionDetail.Status).ToString();
                var personView = mapper.Map<List<SessionPersonView>>(sessionsPersons.Where(y => y.SessionDetailID == item.Key.SessionDetailID && y.UserID == userID).ToList());
                itemdata.sessionPersonViews.AddRange(personView);
                resData.Add(itemdata);

            }
            return new DataResult<List<SessionwithPersonwithDetailModel>>(ResultStatus.Success, resData);
        }

        public   async Task<Result> SessionPersonSlotRemoveAsync(int sessionDetailID)
        {

            var sessionPersons = baseRepository.GetQueryable<SessionPerson>().GetAwaiter().GetResult().Data.Where(y => y.SessionDetailID==sessionDetailID).ToList();
            var   sessionDetail = baseRepository.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => y.ID == sessionDetailID).FirstOrDefault();
            if (sessionDetail is null)
                return new Result(ResultStatus.Warning,"Böyle bir slot bulunamadı");
            if (sessionDetail.Status == 3)
                return new Result(ResultStatus.Warning, "Bu slot onayladığı için silinemez");
            sessionPersons.ForEach(y=> { y.IsActive = false;y.UpdateDate = DateTime.Now; });
             await baseRepository.UpdateManyAsync<SessionPerson>(sessionPersons);
            baseRepository.SaveChanges();

            return new Result(ResultStatus.Success);
        }
    }
}
