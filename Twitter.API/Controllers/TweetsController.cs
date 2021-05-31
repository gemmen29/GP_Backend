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

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        public TweetsController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        // GET: api/Tweets
        [HttpGet]
        public ActionResult<IEnumerable<TweetDetails>> GetTweets(int? page)
        {
            int pageNumber = (page ?? 1);
            return _tweetService.GetTweets(10, pageNumber).ToList();
        }

        // GET: api/MyTweets
        [HttpGet("MyTweets/{id}")]
        public ActionResult<IEnumerable<TweetDetails>> GetMyTweets(string id, int? page)
        {
            int pageNumber = (page ?? 1);
            return _tweetService.GetMyTweets(id, 10, pageNumber).ToList();
        }

        // GET: api/HomePageTweets
        [HttpGet("HomePageTweets/{id}")]
        public ActionResult<IEnumerable<TweetDetails>> GetHomePageTweets(string id, int? page)
        {
            int pageNumber = (page ?? 1);
            return _tweetService.GetHomePageTweets(id,10,pageNumber).ToList();
        }

        // GET: api/Tweets/5
        [HttpGet("{id}")]
        public ActionResult<TweetDetails> GetTweet(int id)
        {
            var tweet = this._tweetService.GetTweet(id);

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
            this._tweetService.PostTweet(addTweetModel);

            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id, addTweetModel);
            return NoContent();
        }

        // POST: api/ReplyToTweet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("ReplyToTweet/{id}")]
        public IActionResult PostReplyToTweet(int id, AddTweetModel addTweetModel)
        {
            this._tweetService.PostReplyToTweet(id, addTweetModel);
            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id }, addTweetModel);
            return NoContent();
        }

        // GET: api/TweetReplies
        [HttpGet("TweetReplies/{id}")]
        public ActionResult<IEnumerable<TweetDetails>> GetTweetReplies(int id)
        {
            return this._tweetService.GetTweetReplies(id).ToList();
        }

        // DELETE: api/Tweets/5
        [HttpDelete("{id}")]
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
