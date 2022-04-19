using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public  interface ISessionsManager
    {

        Task<DataResult<List<SessionsView>>> Search(SimulatorSearcModel dto);
        Task<DataResult<SessionsView>> SimulatorSessionByID(int SessionID);
        Task<DataResult<List<SessionsView>>> ListAsync(List<int> Ids);
        Task<DataResult<List<SessionsView>>> ListAsync();

        Task<DataResult<bool>> AddAsync(Sessions sessions);


    }
}
