
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Claims
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetToken()
            => GetClaim(ClaimTypes.NameIdentifier);

        public string GetClaim(string key)
            => _httpContextAccessor.HttpContext.Request.Headers["token"].ToString();
    }
}
