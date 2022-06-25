using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.AirCraft;
using FindSimulator.Service.Model.Helper;
using FindSimulator.Service.Model.SessionDetail;
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
            CreateMap<SessionPerson, SessionPersonView>().ReverseMap();
            CreateMap<SessionPerson, SessionPersonUpdate>().ReverseMap();
            CreateMap<SessionPerson, SessionPersonAdd>().ReverseMap();
            CreateMap<SimulatorDeviceView, SimulatorDevice>().ReverseMap();
            CreateMap<AirCraft, AirCraftView>().ReverseMap();
            CreateMap<SimulatorDeviceCreate, SimulatorDevice>().ReverseMap();
            CreateMap<SessionDetailView, SessionDetails>().ReverseMap();
            CreateMap<SessionDate, SessionDetails>().ReverseMap();
            CreateMap<SessionDetails, SessionDetailView>();
            CreateMap<SimulatorDevice, SelectObject>().ForMember(dest=>dest.Value,act=>act.MapFrom(src=>src.Code)).
                ForMember(dest => dest.ParentID, act => act.MapFrom(src => src.AirCraftsID));

            CreateMap<UserCreate, Users>();

        }
    }
}
