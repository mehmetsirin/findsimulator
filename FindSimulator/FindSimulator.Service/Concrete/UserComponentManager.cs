using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
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
        private readonly IUserComponentRepository _userComponentRepository;
        private readonly IUsersRepository _usersRepository;

        public UserComponentManager(IBaseManager<int> baseManager, IMapper mapper, IActionScope actionScope, IUserComponentRepository userComponentRepository, IUsersRepository usersRepository)
        {
            _baseManager = baseManager;
            _mapper = mapper;
            _actionScope = actionScope;
            _userComponentRepository = userComponentRepository;
            _usersRepository = usersRepository;
        }

        public  async Task<DataResult<UserWithComponentModel>> GetUserComponentUserByIDsAsync(int userID)
        {
            var response = new UserWithComponentModel();
            var components =  await _baseManager.ListAsync<PageComponent>();
             var  user = _baseManager.GetQueryable<Users>().GetAwaiter().GetResult().Data.Where(y=>y.ID==userID).FirstOrDefault();
            var userComponents = _baseManager.GetQueryable<UserComponent>().GetAwaiter().GetResult().Data.Where(Y=>Y.UserID==userID).ToList();
           
                    var     query=(  
                          from cpn  in components.Data  
                          join  userComp in userComponents  on  cpn.ID  equals  userComp.PageComponentID  into  temp
                          from x in temp.DefaultIfEmpty()
                          select   new UserComponentView {  UserComponentID=x.ID, ComponentName= cpn.Name, IsCreate=x.IsCreate, IsDelete=x.IsDelete, IsRead=x.IsRead, IsWrite=x.IsWrite}).ToList();

            response.userComponentViews = query.ToList();
            response.Email = user.Email;
            response.Surname = user.Surname;
            response.UserName = user.UserName;
            response.UserID = userID;
            //foreach (var item in components.Data)
            //{
                
            //    var response = new UserWithComponentModel();
            //    response.ComponentID = item.ID;
            //    response.ComponentName = item.Name;
            //    response.IsAuthorize = userComponents.Where(y => y.PageComponentID == item.ID).Any();
            //    responses.Add(response);
            //}
            return new DataResult<UserWithComponentModel>(Share.ComplexTypes.ResultStatus.Success,response);
        }

        public  async Task<DataResult<bool>> UpdateAsync(UserWithComponentModel update)
        {
            var user = _usersRepository.GetByIdAsync<Users>(update.UserID).GetAwaiter().GetResult().Data;
            user.Email = update.Email;
            user.UserName = update.UserName;
            user.Surname = update.Surname;
            user.UpdateDate = DateTime.Now;
            _usersRepository.UpdateOne<Users>(user);
              await  _usersRepository.SaveChangesAsync();
            var userComponent = _mapper.Map<List<UserComponent>>(update.userComponentViews.Where(y => y.IsUpdate == true).ToList());
            if (userComponent.Count > 0) 
          await     _userComponentRepository.UpdateManyAsync<UserComponent>(userComponent);
            _userComponentRepository.SaveChanges();
            return new DataResult<bool>();
          
        }
    }
}
