using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.UserComponent;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ComponentController : BaseController
    {
        private readonly IUserComponentManager _userComponentManager;

        public ComponentController(IUserComponentManager userComponentManager)
        {
            _userComponentManager = userComponentManager;
        }

        [HttpGet]
        [Route("get-user-component-user-by-id")]
        public object GetUserComponentUserByIDsAsync(int userID)
        {
            var res =   _userComponentManager.GetUserComponentUserByIDsAsync(userID).GetAwaiter().GetResult();
           
            return res;
         
        }
        [HttpPost]
        [Route("update-user-with-usercomponent")]
        public  async  Task<DataResult<bool>>UpdateUserWithUserComponent(UserWithComponentModel  model)
        {
            var data = _userComponentManager.UpdateAsync(model);

            return new DataResult<bool>();
        }

        
    }
}
