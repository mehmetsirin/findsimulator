using FindSimulator.Service.Model.Users;
using FindSimulator.Share.AppConfiguration;
using FindSimulator.Share.Claims;
using FindSimulator.Share.Results.Concrete;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Api.Jwt
{
    public class JWTAuthenticacationManager : IJWTAuthenticacationManager
    {
        private readonly TokenSettings tokenSettings;

        public JWTAuthenticacationManager(IOptions<TokenSettings> settings)
        {
            tokenSettings = settings.Value;
        }


        public DataResult<UserModelView> Authhenticate(ref DataResult<UserModelView> user)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
           {
                   new Claim(JwtClaimNames.UserName,user.Data.UserName),
                    new Claim(JwtClaimNames.UserID,user.Data.ID.ToString()),
                    new Claim(JwtClaimNames.Surname,user.Data.UserName??""),
                    new Claim(JwtClaimNames.TelNo,user.Data.TelNo??""),
                     new Claim(JwtClaimNames.Email,user.Data.Email??""),
                    new Claim(JwtClaimNames.CompanyID,user.Data.CompanyID.ToString()),
           };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.SigningKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "localhost",
                ValidateAudience = true,
                ValidAudience = "mehmet",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(

                issuer: "localhost",
                audience: "mehmet",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(365)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            user.Data.AccessToken = encodedJwt;
            user.Data.RefreshToken = encodedJwt;

            //var responseJson = new
            //{
            //    access_token = encodedJwt,
            //    expires_in = (int)TimeSpan.FromMinutes(5000).TotalSeconds
            //};
            return user;
        }

        public DataResult<UserLoginWebResponse> AuthhenticateWeb(ref DataResult<UserLoginWebResponse> user)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
          {
                   new Claim(JwtClaimNames.UserName,user.Data.UserName),
                    new Claim(JwtClaimNames.UserID,user.Data.ID.ToString()),
                    new Claim(JwtClaimNames.Surname,user.Data.UserName??""),
                    new Claim(JwtClaimNames.TelNo,user.Data.TelNo??""),
                     new Claim(JwtClaimNames.Email,user.Data.Email??""),
                    new Claim(JwtClaimNames.CompanyID,user.Data.CompanyID.ToString()),
          };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.SigningKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "localhost",
                ValidateAudience = true,
                ValidAudience = "mehmet",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(

                issuer: "localhost",
                audience: "mehmet",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(10)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            user.Data.AccessToken = encodedJwt;
            user.Data.RefreshToken = encodedJwt;

            //var responseJson = new
            //{
            //    access_token = encodedJwt,
            //    expires_in = (int)TimeSpan.FromMinutes(5000).TotalSeconds
            //};
            return user;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SigningKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }


    }
}
