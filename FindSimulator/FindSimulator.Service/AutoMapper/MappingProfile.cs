using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.AutoMapper
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserModelView>();
            CreateMap<UserModelView, Users>();
            CreateMap<UserRegisterModel, Users>();
            CreateMap<Users, UserRegisterModel>();
            CreateMap<UserUpdate, Users>();
            CreateMap<Users, UserUpdate>();
            
        }
    }
}
