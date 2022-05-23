using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Concrete;
using FindSimulator.Service.Model.Users;

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
    public class SimulatorController : BaseController
    {

        public readonly IBaseManager<int> baseManager;
        public readonly ISessionsManager sessionsManager;
        public SimulatorController(IBaseManager<int> baseManager, ISessionsManager sessionsManager)
        {
            this.baseManager = baseManager;
            this.sessionsManager = sessionsManager;
        }


        [HttpGet]
        public async Task<Object> List()
        {
            var res = await this.baseManager.ListAsync<Sessions>();
            return res;

        }

        [HttpPost]
        public async Task<Object> Search([FromBody]SimulatorSearcModel dto)
        {

            var data = await this.sessionsManager.Search(dto);
            return data;
        }
        [HttpGet]
        public async Task<Object> SimulatorSessionByID(int id)
        {
            var data = await this.sessionsManager.SimulatorSessionByID(id);
            return data;
        }
        [HttpGet]
        public  async  Task<object> GetSimulatorDeviceLocation()
        {
            var data = await baseManager.ListAsync<SimulatorDeviceLocation>();
            return data.Data.Where(y => y.CompanyID == 1).ToList();

        }


    }
}
