using FindSimulator.Service.Abstract;
using FindSimulator.Service.Core;
using FindSimulator.Service.Model.Users;

using Microsoft.AspNetCore.Authorization;
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

        public UsersController(IUserManager userManager, BusinessManagerFactory businessManagerFactory):base(businessManagerFactory)
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
        [Authorize]

        public async Task<object> GetUserList()
        {
            var res =  await _userManager.GetUserListAsync();
            return res;
        }

        [HttpPost]
        [Authorize]

        public async Task<Object> Update(UserUpdate dto)
        {
            var res =  await _userManager.Update(dto);
            return res;
        }
        [HttpGet]
        [Authorize]

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
       [HttpPost]
        [Authorize]

        public async Task<object> RemoveAsync( [FromQuery] int  userId)
        {
            var res = await _userManager.ChangeActiveAsync(userId,false);
            return res;
        }
        [HttpPost]
        [Authorize]

        public async Task<object> AddAsync(UserCreate userCreate)
        {
            var res = await _userManager.AddAsync(userCreate);
            return res;
        }
    }
}
