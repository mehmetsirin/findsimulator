using FindSimulator.Service.Abstract;

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
    public class SessionDetailController : ControllerBase
    {
        private ISessionDetailManager sessionDetailManager;
        public SessionDetailController(ISessionDetailManager sessionDetailManager)
        {
            this.sessionDetailManager = sessionDetailManager;
        }

        [HttpGet]
        public async Task<Object> SessionsDetailGet(int sessionID, int simulatorDeviceID)
        {
            var data = await sessionDetailManager.GetSessionDetail(sessionID, simulatorDeviceID);
            return data;
        }
        [HttpGet]
        public   async  Task<object> GetCalendarAsync()
        {
            var data = await sessionDetailManager.GetCalendarAsync();
            return data;
        }
    }
}
