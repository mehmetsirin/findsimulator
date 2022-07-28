using FindSimulator.Infrastructure.Utilities;
using FindSimulator.Service.Core;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{

    public abstract class BaseController : ControllerBase
    {

        public BusinessManagerFactory BusinessManagerFactory { get; }

        public BaseController(BusinessManagerFactory businessManagerFactory)
        {
            BusinessManagerFactory = businessManagerFactory;
        }

       
      
        public string AccessToken
        {
            get
            {
                return HttpContext.Request.Headers["Authorization"];
            }
        }


    }
}
