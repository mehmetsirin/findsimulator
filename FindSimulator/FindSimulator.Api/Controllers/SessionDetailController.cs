using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Core;
using FindSimulator.Service.Model.Session;
using FindSimulator.Service.Model.SessionDetail;
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
    public class SessionDetailController : BaseController
    {
        private ISessionDetailManager sessionDetailManager;
        readonly private ISessionsManager sessionsManager;
        public SessionDetailController(ISessionDetailManager sessionDetailManager, ISessionsManager sessionsManager, BusinessManagerFactory factory):base(factory)
        {
            this.sessionDetailManager = sessionDetailManager;
            this.sessionsManager = sessionsManager;
        }

        [HttpGet]
        public async Task<Object> SessionsDetailGet(int sessionID, int simulatorDeviceID)
        {
            var data = await sessionDetailManager.GetSessionDetail(sessionID, simulatorDeviceID);
            return data;
        }
        //[HttpGet]
        //public   async  Task<object> GetCalendarAsync()
        //{
        //    var data = await sessionDetailManager.GetCalendarAsync();
        //    return data;
        //}

        [HttpPost]
        public  async  Task<object> SessionAdd(SessionCreate create)
        {

            var data =  await sessionDetailManager.SessionAddAsync(create,BusinessManagerFactory._claimService.CompanyID,BusinessManagerFactory._claimService.UserID);
            return data;
        }
        [HttpGet]
        public  async Task<Object> SessionList()
        {
            var data = await sessionsManager.ListAsync();
            return data;
        }

        [HttpPost]
        public  async Task<object> SessionDetailAddAsync(SessionDetailCreate models)
        {
            var data = await sessionDetailManager.SessionDetailAddAsync(models);
            return data;

        }
        [HttpGet]
        public  async  Task<object> SessionwithSessionDetailAsync()
        {
            var data = await sessionDetailManager.SessionwithSessionDetailAsync();
            return data;
        }
        [HttpPost]
        public  async   Task<object> Remove(int id)
        {
            var data = await sessionDetailManager.RemoveAsync(id);
            return data;
        }

         [HttpPost]
         public   async  Task<DataResult<bool>> SessionRemove(int id)
        {
            var data = await sessionsManager.RemoveAsync(id);
            return data;
        }

        [HttpPost]
        public async Task<DataResult<Sessions>> UpdateAsync(SessionUpdate _session)
        {
            var data = await sessionsManager.UpdateAsync(_session);
            return data;
        }

        [HttpPost]
        public  async  Task<DataResult<bool>> SessionStateUpdateAsync( List<SessionDetailStateUpdate> sessionDetailStateUpdates)
        {
            var data =   await sessionDetailManager.SessionStateUpdateAsync(sessionDetailStateUpdates);
            return data;
        }

        [HttpGet]
        public async Task<DataResult<List<SessionDetailWithSessionView>>> GetSessionDetailOrderAsync()
        {
           
          
            var data = await sessionDetailManager.GetSessionDetailOrderAsync();
            return data;
        }

        [HttpPost]
        [Route("")]
        public  async Task<DataResult<bool>> PersonSlotDelete(SessionPersonDelete sessionPersonDelete)
        {
            var data = await sessionDetailManager.SessionPersonWithSessionDetailUpdateAsync(sessionPersonDelete);
            return data;
        }
    }
}

