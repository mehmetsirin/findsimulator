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
    public class SimulatorDeviceController : ControllerBase
    {

        public readonly IBaseManager<int> baseManager;
        public readonly ISimulatorDeviceService simulatorDeviceService;

        public SimulatorDeviceController(IBaseManager<int> baseManager, ISimulatorDeviceService simulatorDeviceService)
        {
            this.baseManager = baseManager;
            this.simulatorDeviceService = simulatorDeviceService;
        }


        [HttpGet]
          public   async  Task<Object> List()
        {
            var data =   await simulatorDeviceService.ListAsync();
            return data;
        }
        [HttpGet]
        public async Task<Object> ListByID(int ID)
        {
            var data = await simulatorDeviceService.ListAsync(ID);
            return data;
        }

    }
}
