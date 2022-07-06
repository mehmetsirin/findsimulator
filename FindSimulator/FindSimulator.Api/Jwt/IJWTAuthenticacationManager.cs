using FindSimulator.Service.Model.Users;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Jwt
{
    public interface IJWTAuthenticacationManager
    {
        DataResult<UserModelView> Authhenticate(ref DataResult<UserModelView> user);
        DataResult<UserLoginWebResponse> AuthhenticateWeb(ref DataResult<UserLoginWebResponse> user);

    }
}
