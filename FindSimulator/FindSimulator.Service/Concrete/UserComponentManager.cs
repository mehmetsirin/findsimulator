using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.UserComponent;
using FindSimulator.Share.Results.Concrete;
using FindSimulator.Share.Scope;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class UserComponentManager : IUserComponentManager
    {

        private readonly IBaseManager<int> _baseManager;
        private readonly IMapper _mapper;
        readonly IActionScope _actionScope;

        public UserComponentManager(IBaseManager<int> baseManager, IMapper mapper, IActionScope actionScope)
        {
            _baseManager = baseManager;
            _mapper = mapper;
            _actionScope = actionScope;
        }

        public  async Task<DataResult<List<UserWithComponentModel>>> GetUserComponentUserByIDsAsync(int userID)
        {
            var responses = new List<UserWithComponentModel>();
            var components =  await _baseManager.ListAsync<PageComponent>();
            var userComponents = _baseManager.GetQueryable<UserComponent>().GetAwaiter().GetResult().Data.Where(Y=>Y.UserID==userID).ToList();
            foreach (var item in components.Data)
            {
                var response = new UserWithComponentModel();
                response.ComponentID = item.ID;
                response.ComponentName = item.Name;
                response.IsAuthorize = userComponents.Where(y => y.PageComponentID == item.ID).Any();
                responses.Add(response);
            }
            return new DataResult<List<UserWithComponentModel>>(Share.ComplexTypes.ResultStatus.Success,responses);
        }

        public  async Task<DataResult<bool>> UpdateAsync(UserComponentUpdate update)
        {
            throw new NotImplementedException();
        }
    }
}
