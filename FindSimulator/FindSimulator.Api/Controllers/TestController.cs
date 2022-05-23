using FindSimulator.Api.Action;
using FindSimulator.Infrastructure.EventBus.Event;
using FindSimulator.Share.Event;
using FindSimulator.Share.RabbitMq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ServiceFilter(typeof(LogEventAction))]

    [ApiController]
    public class TestController : BaseController
    {

        private readonly IEventBus eventBus;
        public int MyProperty { get; set; }
        public TestController(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        [HttpGet]
        public string GetToken(int x=10)
        {

         
            
            return AccessToken;
        }
    }
}
