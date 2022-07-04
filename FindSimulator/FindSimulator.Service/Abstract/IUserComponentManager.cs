using FindSimulator.Service.Model.UserComponent;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public   interface IUserComponentManager
    {

        Task<DataResult<UserWithComponentModel>> GetUserComponentUserByIDsAsync(int userID);
        Task<DataResult<bool>> UpdateAsync(UserWithComponentModel update);

    }
}
