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

        public UserBookmarksService(IUserBookmarksRepository userBookmarksRepository, IMapper mapper) : base(mapper)
        {
            _userBookmarksRepository = userBookmarksRepository;
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

        public List<TweetDetails> GetUserBookmarkedTweets(int pageSize, int pageNumber, string userID)
        {
            return Mapper.Map<TweetDetails[]>(_userBookmarksRepository.GetUserBookmarkedTweets(pageSize, pageNumber, userID)).ToList();
        }
    }
}
