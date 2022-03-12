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
    public class HelperController : ControllerBase
    {
      [HttpGet]
      public  async Task<Object> GetCountryList()
        {
            StreamReader r = new StreamReader("Country.json");
            string jsonString = r.ReadToEnd();
            return jsonString;
        }
    }
}
