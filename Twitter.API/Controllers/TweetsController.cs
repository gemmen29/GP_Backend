using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twitter.Data.Models;
using Twitter.Repository;
using Twitter.Service.Interfaces;
using Twitter.Data.DTOs;
using System.Security.Claims;

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public TweetsController(ITweetService tweetService, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            _tweetService = tweetService;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        // GET: api/Tweets
        [HttpGet]
        public ActionResult<IEnumerable<TweetDetails>> GetTweets(int? page)
        {
            int pageNumber = (page ?? 1);
            return _tweetService.GetTweets(10, pageNumber).ToList();
        }

        // GET: api/MyTweets
        [HttpGet("tweets/{username}/{pageSize}/{pageNumber}")]
        public ActionResult<IEnumerable<TweetDetails>> GetTweets(string username, int? pageSize, int? pageNumber)
        {
            var currentUserID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            var userID = _authService.GetUserID(username).Result;
            return _tweetService.GetMyTweets(userID, currentUserID, pageSize ?? 10, pageNumber ?? 1).ToList();
        }

        //[HttpGet("mytweets/count")]
        //public int GetCount(int? pageSize, int? pageNumber)
        //{
        //    var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
        //    return _tweetService.GetMyTweetsCount(userID);
        //}

        // GET: api/HomePageTweets
        [HttpGet("HomePageTweets/{pageSize}/{pageNumber}")]
        public ActionResult<IEnumerable<TweetDetails>> GetHomePageTweets(int? pageSize, int? pageNumber)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            
            return _tweetService.GetHomePageTweets(userID, pageSize ?? 10, pageNumber ?? 1).ToList();
        }

        // GET: api/Tweets/5
        [HttpGet("{id:int}")]
        public ActionResult<TweetWithReplies> GetTweet(int id)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            var tweet = this._tweetService.GetTweet(userID, id);

            if (tweet == null)
            {
                return NotFound();
            }

            return tweet;
        }

        // POST: api/Tweets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostTweet(AddTweetModel addTweetModel)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            addTweetModel.AuthorId = userID;
            this._tweetService.PostTweet(addTweetModel);

            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id, addTweetModel);
            return NoContent();
        }

        // POST: api/ReplyToTweet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("ReplyToTweet/{id:int}")]
        public IActionResult PostReplyToTweet(int id, AddTweetModel addTweetModel)
        {
            var userID = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            addTweetModel.AuthorId = userID;
            this._tweetService.PostReplyToTweet(id, addTweetModel);
            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id }, addTweetModel);
            return NoContent();
        }

        // GET: api/TweetReplies
        [HttpGet("TweetReplies/{id:int}")]
        public ActionResult<IEnumerable<TweetDetails>> GetTweetReplies(int id)
        {
            return this._tweetService.GetTweetReplies(id).ToList();
        }

        // DELETE: api/Tweets/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteTweet(int id)
        {
            _tweetService.DeleteTweet(id);
            return NoContent();
            
        }

        private bool TweetExists(int id)
        {
            return _tweetService.TweetExists(id);
        }
    }
}
