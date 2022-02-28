using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Concrete;

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

        public AirCraftController(IBaseManager<int> baseManager)
        {
            this.baseManager = baseManager;
        }

        [HttpGet]
        public async   Task<Object> List()
        {
            var res =  await baseManager.ListAsync<AirCraft>();
            return res;
        }
    }
}
