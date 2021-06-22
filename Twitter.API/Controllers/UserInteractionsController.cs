using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;
using Twitter.Service.Interfaces;

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInteractionsController : ControllerBase
    {
        private readonly IUserFollowingService _userFollowingService ;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private readonly IUserLikesService _userLikesService;
        private readonly IUserBookmarksService _userBookmarksService;

        public UserInteractionsController(
            IUserFollowingService userFollowingService,
            IHttpContextAccessor httpContextAccessor,
            IAuthService authService,
            IUserLikesService userLikesService,
            IUserBookmarksService userBookmarksService
            )
        {
            _userFollowingService = userFollowingService;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _userLikesService = userLikesService;
            _userBookmarksService = userBookmarksService;
        }

        [HttpPost("/user/follow/{followingId}")]
        public IActionResult Follow(string followingId)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            //var following = new Following() { FollowerId = userID, FollowingId = _authService.GetUserID(followingUserName).Result };
            var following = new Following() { FollowerId = userID, FollowingId = followingId};
            _userFollowingService.Follow(following);
            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id, addTweetModel);
            return NoContent();
        }

        [HttpPost("/user/unfollow/{followingId}")]
        public IActionResult UnFollow(string followingId)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            var following = new Following() { FollowerId = userID, FollowingId = followingId};
            //var following = new Following() { FollowerId = userID, FollowingId = _authService.GetUserID(followingUserName).Result };
            _userFollowingService.UnFollow(following);

            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id, addTweetModel);
            return NoContent();
        }


        [HttpGet("/user/following/{pageSize}/{pageNumber}")]
        public ActionResult<IEnumerable<UserInteractionDetails>> GetFollowing(int? pageSize, int? pageNumber)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            return _userFollowingService.GetFollowing(pageSize ?? 10, pageNumber ?? 1, userID).ToList();
        }

        [HttpGet("/user/followers/{pageSize}/{pageNumber}")]
        public ActionResult<IEnumerable<UserInteractionDetails>> GetFollowers(int? pageSize, int? pageNumber)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            return _userFollowingService.GetFollowers(pageSize ?? 10, pageNumber ?? 1, userID).ToList();
        }

        [HttpGet("/user/suggestedfollowings")]
        public ActionResult<IEnumerable<UserDetails>> SuggestedFollowings()
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            return _userFollowingService.SuggestedFollowers(userID).ToList();
        }

        [HttpPost("/tweet/like/{tweetId}")]
        public IActionResult Like(int tweetId)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            var userLikes = new UserLikes { UserId = userID, TweetId = tweetId };
            _userLikesService.Like(userLikes);
            return NoContent();
        }

        [HttpPost("/tweet/dislike/{tweetId}")]
        public IActionResult DisLike(int tweetId)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            var userLikes = new UserLikes { UserId = userID, TweetId = tweetId };
            _userLikesService.DisLike(userLikes);
            return NoContent();
        }

        [HttpGet("/tweet/mylikes/{pageSize}/{pageNumber}")]
        public ActionResult<IEnumerable<TweetDetails>> GetMyLikes(int? pageSize, int? pageNumber)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            return _userLikesService.GetUserLikedTweets(pageSize ?? 10, pageNumber ?? 1, userID).ToList();
        }

        [HttpGet("/tweet/tweetlikes/{tweetId}")]
        public ActionResult<IEnumerable<UserInteractionDetails>> GetTweetLikes(int? pageSize, int? pageNumber, int tweetId)
        {
            return _userLikesService.GetTweetLikes(pageSize ?? 10, pageNumber ?? 1, tweetId).ToList();
        }


        [HttpPost("/tweet/bookmark/{tweetId}")]
        public IActionResult Bookmark(int tweetId)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            var userBookmarks = new UserBookmarks { UserId = userID, TweetId = tweetId };
            _userBookmarksService.BookMark(userBookmarks);
            return NoContent();
        }

        [HttpPost("/tweet/removebookmark/{tweetId}")]
        public IActionResult RemoveBookmark(int tweetId)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            var userBookmarks = new UserBookmarks { UserId = userID, TweetId = tweetId };
            _userBookmarksService.RemoveBookMark(userBookmarks);
            return NoContent();
        }


        [HttpGet("/tweet/mybookmarks/{pageSize}/{pageNumber}")]
        public ActionResult<IEnumerable<TweetDetails>> GetMyBookmarks(int? pageSize, int? pageNumber)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            return _userBookmarksService.GetUserBookmarkedTweets(pageSize ?? 10, pageNumber ?? 1, userID).ToList();
        }

        [HttpGet("/tweet/tweetbookmarks/{tweetId}")]
        public ActionResult<IEnumerable<UserInteractionDetails>> GetTweetBookmarks(int? pageSize, int? pageNumber, int tweetId)
        {
            return _userBookmarksService.GetTweetBookmarks(pageSize ?? 10, pageNumber ?? 1, tweetId).ToList();
        }

    }
}
