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
            CreateMap<ApplicationUser, UpdateUserModel>()
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => src.UserPic)
                )
                .ReverseMap();
            CreateMap<Tweet, TweetDetails>()
                .ForMember(
                    dest => dest.LikeCount,
                    opt => opt.MapFrom(src => src.LikedTweets.Count)
                )
                .ForMember(
                    dest => dest.ReplyCount,
                    opt => opt.MapFrom(src => src.Replies.Count)
                )
                .ForMember(
                    dest => dest.BookmarkCount,
                    opt => opt.MapFrom(src => src.BookMarkedTweets.Count)
                )
                .ReverseMap();

            CreateMap<Video, AddVideoModel>().ReverseMap();
            CreateMap<Image, AddImageModel>().ReverseMap();

            CreateMap<Tweet, AddTweetModel>()
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(src => src.Images)
                )
                .ForMember(
                    dest => dest.Video,
                    opt => opt.MapFrom(src => src.Video)
                )
                .ReverseMap();

            CreateMap<Reply, TweetDetails>()
                .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => src.Tweet.Author)
                )
                .ForMember(
                dest => dest.Body,
                opt => opt.MapFrom(src => src.Tweet.Body)
                )
                .ForMember(
                dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.Tweet.CreationDate)
                )
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Tweet.Id)
                )
                .ForMember(
                dest => dest.Images,
                opt => opt.MapFrom(src => src.Tweet.Images)
                )
                .ForMember(
                dest => dest.LikeCount,
                opt => opt.MapFrom(src => src.Tweet.LikedTweets.Count)
                )
                .ForMember(
                dest => dest.ReplyCount,
                opt => opt.MapFrom(src => src.Tweet.Replies.Count)
                )
                .ForMember(
                dest => dest.Video,
                opt => opt.MapFrom(src => src.Tweet.Video)
                ).ReverseMap();
            CreateMap<Tweet, TweetWithReplies>()
                 .ForMember(
                    dest => dest.LikeCount,
                    opt => opt.MapFrom(src => src.LikedTweets.Count)
                )
                .ForMember(
                    dest => dest.ReplyCount,
                    opt => opt.MapFrom(src => src.Replies.Count)
                )
                .ForMember(
                    dest => dest.BookmarkCount,
                    opt => opt.MapFrom(src => src.BookMarkedTweets.Count)
                )
                .ReverseMap();

            CreateMap<ApplicationUser, UserInteractionDetails>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName+" "+src.LastName)
                ).ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => src.UserPic)
                )
                .ReverseMap();
        }
    }
}
