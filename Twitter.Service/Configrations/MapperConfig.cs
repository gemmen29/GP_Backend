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
            CreateMap<Tweet, TweetDetails>()
                .ForMember(
                    dest => dest.LikeCount,
                    opt => opt.MapFrom(src => src.LikedTweets.Count)
                )
                .ForMember(
                    dest => dest.ReplyCount,
                    opt => opt.MapFrom(src => src.Replies.Count)
                )
                .ReverseMap();
            CreateMap<Tweet, AddTweetModel>().ReverseMap();
            CreateMap<Tweet, TweetWithReplies>().ReverseMap();
            CreateMap<ApplicationUser, UserInteractionDetails>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName+" "+src.LastName)
                ).ReverseMap();
        }
    }
}
