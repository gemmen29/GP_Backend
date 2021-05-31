using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;

namespace Twitter.Service.Configrations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDetails>().ReverseMap();
            CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
            CreateMap<UpdateUserModel, ApplicationUser>().ReverseMap();
            CreateMap<Tweet, TweetDetails>().ReverseMap();
            CreateMap<Tweet, AddTweetModel>().ReverseMap();
        }
    }
}
