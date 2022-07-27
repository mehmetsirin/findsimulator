using FindSimulator.Api.Filter;
using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Concrete;
using FindSimulator.Service.Core;
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
    [TransactionActionFilter]
    
    public class AirCraftController : BaseController
    {
        public  readonly IBaseManager<int> baseManager;
        public readonly IAirCraftManager airCraftManager;

        public AirCraftController(IBaseManager<int> baseManager, IAirCraftManager airCraftManager, BusinessManagerFactory factory):base(factory)
        {
            this.baseManager = baseManager;
            this.airCraftManager = airCraftManager;
        }

        [HttpGet]
        public async   Task<Object> List()
        {
            var res = await airCraftManager.List();

            return   new  DataResult<object>(ResultStatus.Success,res);
        }

        [HttpGet]
        public  async  Task<Object> ListGroupAsync()
        {
            var res = await airCraftManager.ListGroupAsync();

            return new DataResult<object>(ResultStatus.Success, res);
        }

        [HttpGet]
        public async Task<Object> SimulatorTypeList()
        {
            var res = await baseManager.ListAsync<SimulatorType>();
            return res;
        }
    }
}
