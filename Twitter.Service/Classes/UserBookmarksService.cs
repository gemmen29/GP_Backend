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
    public class UserBookmarksService : BaseService, IUserBookmarksService
    {
        private IUserBookmarksRepository _userBookmarksRepository { get; }
        private readonly IUserLikesService _userLikesService;
        private readonly IUserBookmarksService _userBookmarksService;

        public UserBookmarksService(
            IUserBookmarksRepository userBookmarksRepository,
            IUserLikesService userLikesService,
            IUserBookmarksService userBookmarksService, 
            IMapper mapper) : base(mapper)
        {
            _userBookmarksRepository = userBookmarksRepository;
            _userLikesService = userLikesService;
            _userBookmarksService = userBookmarksService;
        }

        public void BookMark(UserBookmarks userBookmarks)
        {
            _userBookmarksRepository.BookMark(userBookmarks);
        }

        public void RemoveBookMark(UserBookmarks userBookmarks)
        {
            _userBookmarksRepository.RemoveBookMark(userBookmarks);
        }

        public List<UserInteractionDetails> GetTweetBookmarks(int pageSize, int pageNumber, int tweetID)
        {
            return Mapper.Map<UserInteractionDetails[]>(_userBookmarksRepository.GetTweetBookmarks(pageSize, pageNumber, tweetID)).ToList();
        }

        public IEnumerable<TweetDetails> GetUserBookmarkedTweets(int pageSize, int pageNumber, string userID)
        {
            var tweets = _userBookmarksRepository.GetUserBookmarkedTweets(pageSize, pageNumber, userID);
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

        public bool BookmarkExists(string userId, int tweetId)
        {
            return _userBookmarksRepository.BookmarkExists(userId, tweetId);
        }
    }
}
