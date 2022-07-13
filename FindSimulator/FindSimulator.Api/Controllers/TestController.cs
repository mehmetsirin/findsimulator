using FindSimulator.Api.Action;
using FindSimulator.Api.Filter;
using FindSimulator.Infrastructure.EventBus.Event;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Core;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Event;
using FindSimulator.Share.RabbitMq;
using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ServiceFilter(typeof(LogEventAction))]

    [ApiController]
    [Authorize]
    [TransactionActionFilter]

    public class TestController : BaseController
    {

        private readonly IEventBus eventBus;
        private readonly IUserComponentManager _userComponentManager;
        test1 Test1;
        test2 Test2;

        public TestController(IEventBus eventBus, IUserComponentManager userComponentManager, test2 test2, test1 test1, BusinessManagerFactory factory):base(factory)
        {
            this.eventBus = eventBus;
            _userComponentManager = userComponentManager;
            Test1 = test1;
            Test2 = test2;
        }

        [HttpGet]
        public string GetToken(int x=10)
        {
            return AccessToken;
        }

        [HttpGet]
        public string GetToken1(int x = 11)
        {
            return "Mehmet";
        }
        [HttpGet]
        public object Getaaa()
        {
            //    var pets = new List<Pet>
            //{
            //    new Pet { Type = "Cat", Name = "MooMoo", Age = 3.4 },
            //    new Pet { Type = "Squirrel", Name = "Sandy", Age = 7 }
            //};
            //    var person = new Person
            //    {
            //        Name = "John",
            //        Age = 34,
            //        StateOfOrigin = "England",
            //        Pets = pets
            //    };
            //    var options = new JsonSerializerOptions
            //    {
            //        WriteIndented = true
            //    };
            //    return JsonSerializer.Serialize(  new DataResult<Person>(person), options);
            var res = _userComponentManager.GetUserComponentUserByIDsAsync(1).GetAwaiter().GetResult();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return res;
        }
        [HttpGet]
        public object Getact()
        {
            var tes = Test1.Get();
            var tesx = Test2.Get();
            return tes + "-" + tesx;
        }
        [HttpGet]
        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


    }

    public class Pet
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
    }
    public class Person
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string StateOfOrigin { get; set; }
        public List<Pet> Pets { get; set; }
    }

}
