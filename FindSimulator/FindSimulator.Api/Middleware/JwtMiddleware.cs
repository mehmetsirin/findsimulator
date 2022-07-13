using FindSimulator.Share.AppConfiguration;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var userId = jwtUtils.ValidateJwtToken(token)/*;*/
            //if (userId != null)
            //{
            //    // attach user to context on successful jwt validation
            //    //context.Items["User"] = userService.GetById(userId.Value)/*;*/
            //}
            //else
            //    throw new Exception("sasas");

            await _next(context);
        }
    }
}
