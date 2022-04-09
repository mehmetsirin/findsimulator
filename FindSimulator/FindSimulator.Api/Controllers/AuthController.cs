using FindSimulator.Api.Jwt;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

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
    public class AuthController : ControllerBase
    {
        readonly IUserManager userManager;
        public IJWTAuthenticacationManager JWTAuthenticacation;

        public AuthController(IUserManager _userManager, IJWTAuthenticacationManager jWTAuthenticacation)
        {
            userManager = _userManager;

            JWTAuthenticacation = jWTAuthenticacation;
        }
        [HttpPost]
        public async  Task<Object> Login(UserLoginModel dto)
        {
            var response = await userManager.Login(dto.email, dto.pass,"");
            if (response.ResultStatus == ResultStatus.Success)
            
               JWTAuthenticacation.Authhenticate(ref response);
            return response;

        }
      
    }
}
