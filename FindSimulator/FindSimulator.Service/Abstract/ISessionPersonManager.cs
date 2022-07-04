using FindSimulator.Service.Model.SessionDetail;
using FindSimulator.Service.Model.SessionPerson;
using FindSimulator.Service.Model.SimulatorDevice;
using FindSimulator.Share.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public partial interface ISessionPersonManager
    {
        Task<DataResult<List<SessionPersonView>>> ListAsync();
        Task<DataResult<List<SessionwithPersonwithDetailModel>>> GetUserByIDSessions(int userID);

        Task<DataResult<List<SessionPersonView>>> ListAsync(int SessionID,int SessionDetailID);
        Task<DataResult<SessionPersonView>> GetByIDAsync(int id);
        Task<DataResult<List<SessionPersonView>>> ListAsync(int SessionID);

        Task<DataResult<bool>> AddMultipleAsync(List<SessionPersonAdd> adds, int userID);
        Task<DataResult<bool>> AddAsync(SessionPersonAdd add);

        Task<Result> UpdateAsync(SessionPersonUpdate update);
        Task<Result> Remove(int ID);
        Task<Result> SessionPersonSlotRemoveAsync(int sessionDetail);


    }
}
