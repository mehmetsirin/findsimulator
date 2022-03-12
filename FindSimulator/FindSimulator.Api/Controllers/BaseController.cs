using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{

    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        public Guid CompanyID
        {
            get
            {

                return new Guid(HttpContext.User.Claims.Where(a => a.Type == "companyId").Select(x => x.Value).FirstOrDefault());
            }
        }


        public Guid UserID
        {
            get
            {
                return new Guid(HttpContext.User.Claims.Where(a => a.Type == "userId").Select(x => x.Value).FirstOrDefault());
            }
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
