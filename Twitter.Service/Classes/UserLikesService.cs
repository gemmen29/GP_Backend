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
        private readonly IUserLikesService _userLikesService;
        private readonly IUserBookmarksService _userBookmarksService;

        public UserLikesService(
            IUserLikesRepository userLikesRepository,
            IUserLikesService userLikesService,
            IUserBookmarksService userBookmarksService, 
            IMapper mapper) : base(mapper)
        {
            _userLikesRepository = userLikesRepository;
            _userLikesService = userLikesService;
            _userBookmarksService = userBookmarksService;
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

        public IEnumerable<TweetDetails> GetUserLikedTweets(int pageSize, int pageNumber, string userID)
        {
            var tweets = _userLikesRepository.GetUserLikedTweets(pageSize, pageNumber, userID);
            // trival solution
            var tweetsDetails = Mapper.Map<TweetDetails[]>(tweets);
            for (int i = 0; i < tweetsDetails.Count(); i++)
            {
                tweetsDetails[i].IsLiked = _userLikesService.LikeExists(userID, tweetsDetails[i].Id);
                tweetsDetails[i].IsBookmarked = _userBookmarksService.BookmarkExists(userID, tweetsDetails[i].Id);
                //tweetsDetails[i].Author.IsFollowedByCurrentUser = (userID == tweetsDetails[i].Author.Id) || _userFollowingService.FollowingExists(userId, tweetsDetails[i].Author.Id);
            }
            return tweetsDetails;
        }

        public bool LikeExists(string userId, int tweetId)
        {
            return _userLikesRepository.LikeExists(userId, tweetId);
        }
    }
}
