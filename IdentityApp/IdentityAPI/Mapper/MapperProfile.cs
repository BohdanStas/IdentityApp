using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using IdentityAPI.Commands;
using IdentityAPI.Models;

namespace IdentityAPI.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Register.Command, User>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
