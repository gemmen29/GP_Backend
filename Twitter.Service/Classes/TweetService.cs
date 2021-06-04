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
        private ITweetRepository _tweetRepository { get; }

        public TweetService(ITweetRepository tweetRepository, IMapper mapper): base(mapper)
        {
            _tweetRepository = tweetRepository;
        }
        public void DeleteTweet(int id)
        {
            _tweetRepository.DeleteTweet(id);
        }

        public TweetWithReplies GetTweet(int id)
        {
            var tweet = _tweetRepository.GetTweet(id);
            return Mapper.Map<TweetWithReplies>(tweet);
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

        public IEnumerable<TweetDetails> GetHomePageTweets(string id, int pageSize, int pageNumber)
        {
            var tweets = _tweetRepository.GetHomePageTweets(id, pageSize, pageNumber);
            return Mapper.Map<TweetDetails[]>(tweets);
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

    }
}
