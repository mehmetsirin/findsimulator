using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Users;

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
    public class UsersController : BaseController
    {
        readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;

        }
        [HttpPost]
        public async Task<Object> Register(UserRegisterModel dto)
        {
            var res = _userManager.Register(dto);
            return res;
        }

        [HttpGet]
        public async Task<object> GetUserList()
        {
            var res =  await _userManager.GetUserListAsync();
            return res;
        }

        [HttpPost]
        public async Task<Object> Update(UserUpdate dto)
        {
            var res =  await _userManager.Update(dto);
            return res;
        }
        [HttpGet]
        public async Task<Object> GetUserID(int userID)
        {
            var res =  await _userManager.GetUserID(userID);
            return res;
        }

        [HttpPost]
        public async Task<Object> Confirm( string key)
        {

            var res = await _userManager.Confirm(key);
            return res;
        }
    }
}
