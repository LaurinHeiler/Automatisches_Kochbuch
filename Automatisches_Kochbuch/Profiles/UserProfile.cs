using AutoMapper;
using Automatisches_Kochbuch.Dtos;
using Automatisches_Kochbuch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<TabUser, UserReadDto>();
            CreateMap<UserCreateDto, TabUser>();
            CreateMap<UserUpdateDto, TabUser>();
        }
    }
}
