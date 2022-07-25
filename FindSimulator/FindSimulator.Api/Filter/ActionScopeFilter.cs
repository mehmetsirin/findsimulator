using FindSimulator.Share.Claims;
using FindSimulator.Share.Scope;

using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Linq;
namespace FindSimulator.Api.Filter
{
    public class ActionScopeFilter : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Path.Value == "/api/Auth/Login" || context.HttpContext.Request.Path.Value == "/api/auth/LoginWeb")
            {
                return;
            }

            var actionScope = context.HttpContext.RequestServices.GetService(typeof(IClaimService)) as IClaimService;
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            var userId = int.Parse(claimsIdentity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.UserID)?.Value);
            var userName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.UserName)?.Value;
            var email = claimsIdentity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.Email)?.Value;
            var surname = claimsIdentity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.Surname)?.Value;
            var telNo = claimsIdentity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.TelNo)?.Value;
            var countryCode = claimsIdentity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.CountryCode)?.Value;
            var companyID = claimsIdentity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.CompanyID)?.Value;

            actionScope.SetInit(userName, email, surname, telNo, userId);
            actionScope.CompanyID = companyID == null ? 0 : int.Parse(companyID);
        }
    }
}
