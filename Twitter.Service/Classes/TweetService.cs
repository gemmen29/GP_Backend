using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;
using Twitter.Repository;
using Twitter.Repository.classes;
using Twitter.Repository.Interfaces;
using Twitter.Service.Interfaces;

namespace Twitter.Service.Classes
{
    public class TweetService : BaseService, ITweetService
    {
        private readonly IUserLikesService _userLikesService;
        private readonly IUserBookmarksService _userBookmarksService;
        private readonly IUserFollowingService _userFollowingService;

        private ITweetRepository _tweetRepository { get; }

        public TweetService(
            ITweetRepository tweetRepository,
            IUserLikesService userLikesService, 
            IUserBookmarksService userBookmarksService,
            IUserFollowingService userFollowingService,
            IMapper mapper
            ): base(mapper)
        {
            _tweetRepository = tweetRepository;
            _userLikesService = userLikesService;
            _userBookmarksService = userBookmarksService;
            _userFollowingService = userFollowingService;
        }
        public void DeleteTweet(int id)
        {
            _tweetRepository.DeleteTweet(id);
        }

        public TweetWithReplies GetTweet(string userId, int tweetId)
        {
            var tweet = _tweetRepository.GetTweet(tweetId);
            // trival solution
            var tweetWithReplies = Mapper.Map<TweetWithReplies>(tweet);
            tweetWithReplies.IsLiked = _userLikesService.LikeExists(userId, tweetId);
            tweetWithReplies.IsBookmarked = _userBookmarksService.BookmarkExists(userId, tweetId);
            for (int i = 0; i < tweetWithReplies.Replies.Count(); i++)
            {
                tweetWithReplies.Replies[i].IsLiked = _userLikesService.LikeExists(userId, tweetWithReplies.Replies[i].Id);
                tweetWithReplies.Replies[i].IsBookmarked = _userBookmarksService.BookmarkExists(userId, tweetWithReplies.Replies[i].Id);
            }
            return tweetWithReplies;
        }

        public IEnumerable<TweetDetails> GetTweetReplies(int id)
        {
            var tweets = _tweetRepository.GetTweetReplies(id);
            return Mapper.Map<TweetDetails[]>(tweets);
        }

        public IEnumerable<TweetDetails> GetTweets(int pageSize, int pageNumber)
        {
            var tweets= _tweetRepository.GetTweets(pageSize, pageNumber);
            return Mapper.Map<TweetDetails[]>(tweets);
        }

        public int GetTweetsCount()
        {
            return _tweetRepository.GetTweetsCount();
        }

        public IEnumerable<TweetDetails> GetMyTweets(string id, int pageSize, int pageNumber)
        {
            var tweets = _tweetRepository.GetMyTweets(id, pageSize, pageNumber);
            return Mapper.Map<TweetDetails[]>(tweets);
        }

        public IEnumerable<TweetDetails> GetHomePageTweets(string userId, int pageSize, int pageNumber)
        {
            var tweets = _tweetRepository.GetHomePageTweets(userId, pageSize, pageNumber);
            // trival solution
            var tweetsDetails = Mapper.Map<TweetDetails[]>(tweets);
            for (int i = 0; i < tweetsDetails.Count(); i++)
            {
                tweetsDetails[i].IsLiked = _userLikesService.LikeExists(userId, tweetsDetails[i].Id);
                tweetsDetails[i].IsBookmarked = _userBookmarksService.BookmarkExists(userId, tweetsDetails[i].Id);
                tweetsDetails[i].Author.IsFollowedByCurrentUser = (userId == tweetsDetails[i].Author.Id) || _userFollowingService.FollowingExists(userId, tweetsDetails[i].Author.Id);
            }
            return tweetsDetails;
        }

        public TweetDetails PostReplyToTweet(int id, AddTweetModel addTweetModel)
        {
            var tweetModel = Mapper.Map<Tweet>(addTweetModel);
            Tweet tweet = _tweetRepository.PostReplyToTweet(id, tweetModel).Result;
            return Mapper.Map<TweetDetails>(tweet);
        }

        public TweetDetails PostTweet(AddTweetModel addTweetModel)
        {
            var tweetModel = Mapper.Map<Tweet>(addTweetModel);
            Tweet tweet =  _tweetRepository.PostTweet(tweetModel).Result;
            return Mapper.Map<TweetDetails>(tweet);
        }

        public bool TweetExists(int id)
        {
            return _tweetRepository.TweetExists(id);
        }

        public int GetMyTweetsCount(string id)
        {
            return _tweetRepository.GetMyTweetsCount(id);
        }
    }
}
