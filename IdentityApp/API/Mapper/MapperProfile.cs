using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Commands;
using API.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.Win32;

namespace API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Register.Command, User>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
