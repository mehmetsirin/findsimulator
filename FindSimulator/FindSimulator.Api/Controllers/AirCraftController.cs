using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Concrete;
using FindSimulator.Share.ComplexTypes;
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
    public class AirCraftController : ControllerBase
    {
        public  readonly IBaseManager<int> baseManager;
        public readonly IAirCraftManager airCraftManager;

        public AirCraftController(IBaseManager<int> baseManager, IAirCraftManager airCraftManager)
        {
            this.baseManager = baseManager;
            this.airCraftManager = airCraftManager;
        }

        [HttpGet]
        public async   Task<Object> List()
        {
            var res = await airCraftManager.ListGroupAsync();

            return   new  DataResult<object>(ResultStatus.Success,res);
        }

        [HttpGet]
        public async Task<Object> SimulatorTypeList()
        {
            var res = await baseManager.ListAsync<SimulatorType>();
            return res;
        }
    }
}
