using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Core;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HelperController : BaseController
    {

        public readonly IBaseManager<int> baseManager;

        public HelperController(IBaseManager<int> baseManager, BusinessManagerFactory factory):base(factory)
        {
            this.baseManager = baseManager;
        }

        [HttpGet]
      public  async Task<Object> GetCountryList()
        {
            StreamReader r = new StreamReader("Country.json");
            string jsonString = r.ReadToEnd();
            return jsonString;
        }

      [HttpGet]
      public  async  Task<object> GetManufacturers()
        {
            var data =  await baseManager.ListAsync<Manufacturer>();

            return data;
        }
    }
}
