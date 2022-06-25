using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public  interface IUserManager
    {
        Task<DataResult<UserModelView>> Login(string email, string pass, string  deviceID=null);

        Task<DataResult<bool>> Register(UserRegisterModel user);
        Task<DataResult<UserModelView>> GetUserID(int id);
        Task<DataResult<List<UserModelView>>> GetUserListAsync();
        Task<DataResult<bool>> Update(UserUpdate dto);
        Task<DataResult<bool>> Update(Users dto);
        Task<DataResult<bool>> Confirm(string key);
        Task<DataResult<bool>> ChangeActiveAsync(int userID, bool isActive);
    }
}
