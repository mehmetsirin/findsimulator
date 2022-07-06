using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Utilities;
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
        //private readonly IUserComponentRepository _userComponentRepository;
        //private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserComponentManager(IBaseManager<int> baseManager, IMapper mapper, IActionScope actionScope, IUnitOfWork unitOfWork)
        {
            _baseManager = baseManager;
            _mapper = mapper;
            _actionScope = actionScope;
            //_userComponentRepository = userComponentRepository;
            //_usersRepository = usersRepository;
         this._unitOfWork=unitOfWork;
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
                          select   new UserComponentView { PageComponentID = cpn.ID, ComponentName= cpn.Name, IsCreate= x==null? true:x.IsCreate, IsDelete=x==null?true:x.IsDelete, IsRead=x==null? true:x.IsRead, IsWrite=x==null?true:x.IsWrite}).ToList();

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
            var user = _unitOfWork.UserComponentRepository.GetByIdAsync<Users>(update.UserID).GetAwaiter().GetResult().Data;
        
            user.Email = update.Email;
            user.UserName = update.UserName;
            user.Surname = update.Surname;
            user.UpdateDate = DateTime.Now;
            //_unitOfWork.UsersRepository.UpdateOne<Users>(user);
            //await  _unitOfWork.UserComponentRepository.SaveChangesAsync();
            var userComponents = _unitOfWork.UserComponentRepository.GetQueryable<UserComponent>().GetAwaiter().GetResult().Data.Where(y => y.UserID == user.ID).ToList();
            var userComponentMap = _mapper.Map<List<UserComponent>>(update.userComponentViews.Where(y => y.IsUpdate == true).ToList());
            if (userComponentMap.Count > 0)
            {
                foreach (var item in userComponentMap)
                {
                    var userComponent = userComponents.Where(y => y.PageComponentID == item.PageComponentID).FirstOrDefault();

                    if(userComponent is not null)
                    {
                        userComponent.IsCreate = item.IsCreate;
                        userComponent.IsDelete = item.IsDelete;
                        userComponent.IsRead = item.IsRead;
                        userComponent.IsWrite = item.IsWrite;
                        userComponent.UpdateDate = DateTime.Now;
                        _unitOfWork.UserComponentRepository.UpdateOne<UserComponent>(userComponent);
                    }
                    else
                    {
                        item.UserID = update.UserID;
                        _unitOfWork.UserComponentRepository.AddOne<UserComponent>(item);

                    }
                }
            }
            _unitOfWork.Complete();
            return new DataResult<bool>();
          
        }
    }
}
