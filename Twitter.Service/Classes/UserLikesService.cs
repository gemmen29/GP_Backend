using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;
using Twitter.Repository.Interfaces;
using Twitter.Service.Interfaces;

namespace Twitter.Service.Classes
{
    public class UserLikesService : BaseService, IUserLikesService
    {
        private IUserLikesRepository _userLikesRepository { get; }

        public UserLikesService(IUserLikesRepository userLikesRepository, IMapper mapper) : base(mapper)
        {
            _userLikesRepository = userLikesRepository;
        }

        public void Like(UserLikes userLikes)
        {
            _userLikesRepository.Like(userLikes);
        }

        public void DisLike(UserLikes userLikes)
        {
            _userLikesRepository.DisLike(userLikes);
        }

        public List<UserInteractionDetails> GetTweetLikes(int pageSize, int pageNumber, int tweetID)
        {
            return Mapper.Map<UserInteractionDetails[]>(_userLikesRepository.GetTweetLikes(pageSize, pageNumber, tweetID)).ToList();
        }

        public List<TweetDetails> GetUserLikedTweets(int pageSize, int pageNumber, string userID)
        {
            return Mapper.Map<TweetDetails[]>(_userLikesRepository.GetUserLikedTweets(pageSize, pageNumber,userID)).ToList();
        }
    }
}
