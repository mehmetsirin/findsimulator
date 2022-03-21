using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.SessionPerson;
using FindSimulator.Service.Model.SimulatorDevice;
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

            CreateMap<Sessions, SessionsView>();
            CreateMap<SessionsView, Sessions>();
            CreateMap<SimulatorDeviceView, SimulatorDevice>();
            CreateMap< SimulatorDevice, SimulatorDeviceView>();
            CreateMap<SessionPerson, SessionPersonView>().ReverseMap();
            CreateMap<SessionPerson, SessionPersonUpdate>().ReverseMap();
            CreateMap<SessionPerson, SessionPersonAdd>().ReverseMap();
          


        }
    }
}
