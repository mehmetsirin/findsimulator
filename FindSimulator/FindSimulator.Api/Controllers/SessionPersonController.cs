using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.SessionPerson;
using FindSimulator.Share.Results.Concrete;

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
    public class SessionPersonController : BaseController
    {
        private readonly ISessionPersonManager sessionPersonManager;

        public SessionPersonController(ISessionPersonManager sessionPersonManager)
        {
            this.sessionPersonManager = sessionPersonManager;
        }


        [HttpGet]

        public Task<DataResult<List<SessionPersonView>>> ListAsync()
        {
            var data = sessionPersonManager.ListAsync();
            return data;

        }
        [HttpGet]
       public   Task<DataResult<List<SessionPersonView>>> ListSessionDetailIDAsync(int SessionID, int SessionDetailID)
        {
            var data = sessionPersonManager.ListAsync(SessionID,SessionDetailID);
            return data;
        }
        [HttpGet]

      public   Task<DataResult<SessionPersonView>> GetByIDAsync(int id)
        {
            var data = sessionPersonManager.GetByIDAsync(id);
            return data;
        }
        [HttpGet]

        public Task<DataResult<List<SessionPersonView>>> ListSessionByIDAsync(int SessionID)
        {
            var data = sessionPersonManager.ListAsync(SessionID);
            return data;
        }
        [HttpPost]
        public Task<DataResult<bool>> AddAsync(SessionPersonAdd adds)
        {
            var data = sessionPersonManager.AddAsync(adds);
            return data;
        }
        [HttpPost]
        public Task<DataResult<bool>> AddMultipleAsync(List<SessionPersonAdd> adds)
        {
            var data = sessionPersonManager.AddMultipleAsync(adds);
            return data;
        }
        [HttpPost]

        public Task<Result> UpdateAsync(SessionPersonUpdate update)
        {
            var data = sessionPersonManager.UpdateAsync(update);
            return data;
        }
        [HttpPost]

        public Task<Result> Remove(int ID)
        {
            var data = sessionPersonManager.Remove(ID);
            return data;
        }
    }
}
