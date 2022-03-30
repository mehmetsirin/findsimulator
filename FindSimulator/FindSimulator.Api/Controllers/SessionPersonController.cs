using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.SessionPerson;
using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SessionPersonController : BaseController
    {
        private readonly ISessionPersonManager sessionPersonManager;

        public SessionPersonController(ISessionPersonManager sessionPersonManager)
        {
            this.sessionPersonManager = sessionPersonManager;
        }


        [HttpGet]

        public async Task<DataResult<List<SessionPersonView>>> ListAsync()
        {
            var data = await sessionPersonManager.ListAsync();
            return data;

        }
        [HttpGet]
       public   async  Task<DataResult<List<SessionPersonView>>> ListSessionDetailIDAsync(int SessionID, int SessionDetailID)
        {
            var data = await sessionPersonManager.ListAsync(SessionID,SessionDetailID);
            return data;
        }
        [HttpGet]

      public   async  Task<DataResult<SessionPersonView>> GetByIDAsync(int id)
        {
            var data = await sessionPersonManager.GetByIDAsync(id);
            return data;
        }
        [HttpGet]

        public   async Task<DataResult<List<SessionPersonView>>> ListSessionByIDAsync(int SessionID)
        {
            var data =  await sessionPersonManager.ListAsync(SessionID);
            return data;
        }
        [HttpPost]
        public async Task<DataResult<bool>> AddAsync(SessionPersonAdd adds)
        {
            var data =   await sessionPersonManager.AddAsync(adds);
            return data;
        }
        [HttpPost]
        public   async Task<DataResult<bool>> AddMultipleAsync(List<SessionPersonAdd> adds)
        {
         
            var data = await sessionPersonManager.AddMultipleAsync(adds,UserID);
            return data;
        }
        [HttpPost]

        public   async Task<Result> UpdateAsync(SessionPersonUpdate update)
        {
            var data =  await sessionPersonManager.UpdateAsync(update);
            return data;
        }
        [HttpPost]

        public Task<Result> Remove(int ID)
        {
            var data = sessionPersonManager.Remove(ID);
            return data;
        }

        [HttpGet]
         public  async  Task<Object> GetUserSessions()
        {
            var data =   await  sessionPersonManager.GetUserByIDSessions(UserID);
            return data;
        }
    }
}
