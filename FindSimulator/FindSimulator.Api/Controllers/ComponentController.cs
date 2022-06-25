using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.UserComponent;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<DataResult<List<UserWithComponentModel>>> GetUserComponentUserByIDsAsync(int userID)
        {
            var res =  await _userComponentManager.GetUserComponentUserByIDsAsync(userID);
            return res;
        }
        [HttpPost]
        [Route("update-user-with-usercomponent")]
        public  async  Task<DataResult<bool>>UpdateUserWithUserComponent(UserWithUserComponentUpdate componentUpdate)
        {

            return new DataResult<bool>();
        }
        
    }
}
