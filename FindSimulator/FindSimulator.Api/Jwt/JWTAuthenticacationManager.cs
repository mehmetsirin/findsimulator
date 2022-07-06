using FindSimulator.Service.Model.Users;
using FindSimulator.Share.AppConfiguration;
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
                   new Claim("name",user.Data.UserName),
                    new Claim("userId",user.Data.ID.ToString()),

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

        public DataResult<UserLoginWebResponse> AuthhenticateWeb(ref DataResult<UserLoginWebResponse> user)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
          {
                   new Claim("name",user.Data.UserName),
                    new Claim("userId",user.Data.ID.ToString()),

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
    }
}
