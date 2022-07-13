using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Notification;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Users;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;
using FindSimulator.Share.Scope;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class UsersManager : IUserManager
    {
        readonly IUsersRepository usersRepository;
        readonly IMapper mapper;
        readonly IRedisManager redisManager;
        readonly INotificationRepository notificationRepository;
        readonly IActionScope _actionScope;
        private IUserComponentManager _userComponentManager;

        public UsersManager(IUsersRepository usersRepository, IMapper _mapper, IRedisManager redisManager, INotificationRepository notificationRepository, IActionScope actionScope, IUserComponentManager userComponentManager)
        {
            this.usersRepository = usersRepository;
            this.mapper = _mapper;
            this.redisManager = redisManager;
            this.notificationRepository = notificationRepository;
            _actionScope = actionScope;
            _userComponentManager = userComponentManager;
        }

        public async Task<DataResult<bool>> AddAsync(UserCreate create)
        {

            var user = mapper.Map<Users>(create);
            user.CompanyID = _actionScope.IdCompany;
            await usersRepository.AddOneAsync<Users>(user);
            usersRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Authority, true);
        }

        public async Task<DataResult<bool>> ChangeActiveAsync(int userID, bool isActive)
        {
            var user = usersRepository.GetByIdAsync<Users>(userID).GetAwaiter().GetResult().Data;
            user.IsActive = false;
            await Update(user);
            usersRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success);
        }

        public async Task<DataResult<bool>> Confirm(string key)
        {
            var redisData = await redisManager.Get(key);
            if (redisData != null && redisData.ResultStatus == 0 && redisData.Data.UserID != 0)
            {

                var user = await usersRepository.GetByIdAsync<Users>(redisData.Data.UserID);
                user.Data.IsActive = true;
                usersRepository.UpdateOne<Users>(user.Data);
                usersRepository.SaveChanges();
                return new DataResult<bool>(ResultStatus.Success);

            }
            else
            {
                return new DataResult<bool>(ResultStatus.Error, "Yanlış Code  Girdiniz");
            }

        }

        public async Task<DataResult<UserModelView>> GetUserID(int id)
        {

            var data = await usersRepository.GetByIdAsync<Users>(id);
            var dataRes = mapper.Map<UserModelView>(data.Data);
            if (dataRes == null)
                return new DataResult<UserModelView>(ResultStatus.DataNull);

            dataRes.FullName = dataRes?.UserName + " " + dataRes?.Surname;
            return new DataResult<UserModelView>(ResultStatus.Success, dataRes);
        }
        public async Task<DataResult<List<UserModelView>>> GetUserListAsync()
        {
            var data = usersRepository.GetQueryable<Users>().GetAwaiter().GetResult().Data.Where(y => y.CompanyID == _actionScope.IdCompany).ToList();
            var dataRes = mapper.Map<List<UserModelView>>(data);
            return new DataResult<List<UserModelView>>(ResultStatus.Success, dataRes);
        }

        public async Task<DataResult<UserModelView>> Login(string email, string pass, string deviceID = null)
        {

            try
            {
                var user = this.usersRepository.List<Users>().GetAwaiter().GetResult().Data.Where(y => y.Password == pass && y.Email == email).FirstOrDefault();


                if (user != null)
                {
                    UserModelView modelView = mapper.Map<UserModelView>(user);
                    user.DeviceID = deviceID;
                    await Update(user);

                    return new DataResult<UserModelView>(ResultStatus.Success, modelView);
                }
                else
                {
                    return new DataResult<UserModelView>(ResultStatus.DataNull, "Boyle Bir Kullanıcı Bulunamadı");
                }
            }
            catch (Exception ex)
            {

                return new DataResult<UserModelView>(ResultStatus.Error, ex.Message);
            }
            //return new DataResult<UserModelView>(ResultStatus.Error, "Kullanıcı Bilgileriniz  Yanlış");
        }

        public async Task<DataResult<UserLoginWebResponse>> LoginWebAsync(string email, string pass)
        {
            var user = this.usersRepository.List<Users>().GetAwaiter().GetResult().Data.Where(y => y.Password == pass && y.Email == email).FirstOrDefault();


            if (user != null)
            {
                var modelView = mapper.Map<UserLoginWebResponse>(user);
                var  userComponent=  await _userComponentManager.GetUserComponentUserByIDsAsync(user.ID);
                modelView.userWithComponentModel = userComponent.Data;
                //await Update(user);
                return new DataResult<UserLoginWebResponse>(ResultStatus.Success, modelView);
            }
            else
            {
                return new DataResult<UserLoginWebResponse>(ResultStatus.DataNull, "Boyle Bir Kullanıcı Bulunamadı");

            }
        }

        public async Task<DataResult<bool>> Register(UserRegisterModel _user)
        {
            var user = mapper.Map<Users>(_user);
            user.IsActive = false;

            var res = this.usersRepository.AddOne<Users>(user);
            var effected = this.usersRepository.SaveChanges();
            if (effected > 0)
            {
                Random random = new Random();
                var code = random.Next(5000, 99999).ToString();
                await redisManager.Set(code, new RedisModel() { UserID = user.ID, InsertDateTime = DateTime.Now }, DateTime.Now.AddMinutes(7));

                await notificationRepository.SenMailOutlook(code, user.Email, user);
            }
            return new DataResult<bool>(ResultStatus.Success, true);
        }

        public async Task<DataResult<bool>> Update(UserUpdate dto)
        {
            var user = mapper.Map<Users>(dto);
            var res = this.usersRepository.UpdateOne<Users>(user);
            var effected = this.usersRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success, true);
        }

        public async Task<DataResult<bool>> Update(Users dto)
        {
            var res = this.usersRepository.UpdateOne<Users>(dto);
            var effected = this.usersRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success, true);
        }
    }
}

