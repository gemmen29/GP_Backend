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
using PagedList;


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
        public ActionResult<IEnumerable<Tweet>> GetTweets(int? page)
        {
            int pageNumber = (page ?? 1);
            return _tweetService.GetTweets().ToPagedList(pageNumber,10).ToList();
        }

        // GET: api/MyTweets
        [HttpGet("MyTweets/{id}")]
        public ActionResult<IEnumerable<Tweet>> GetMyTweets(string id, int? page)
        {
            int pageNumber = (page ?? 1);
            return _tweetService.GetMyTweets(id).ToPagedList(pageNumber, 10).ToList();
        }

        // GET: api/HomePageTweets
        [HttpGet("HomePageTweets/{id}")]
        public ActionResult<IEnumerable<Tweet>> GetHomePageTweets(string id, int? page)
        {
            int pageNumber = (page ?? 1);
            return _tweetService.GetHomePageTweets(id).ToPagedList(pageNumber, 10).ToList();
        }

        // GET: api/Tweets/5
        [HttpGet("{id}")]
        public ActionResult<Tweet> GetTweet(int id)
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
        public async Task<ActionResult<Tweet>> PostTweet(string authorId, Tweet tweet)
        {
            await this._tweetService.PostTweet(authorId, tweet);

            return CreatedAtAction("GetTweet", new { id = tweet.Id }, tweet);
        }

        // POST: api/ReplyToTweet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("ReplyToTweet/{id}")]
        public async Task<ActionResult<Tweet>> PostReplyToTweet(int id, string authorId, Tweet tweet)
        {
            return CreatedAtAction("GetTweet", new { id = tweet.Id }, await this._tweetService.PostReplyToTweet(id, authorId, tweet));
        }

        // GET: api/TweetReplies
        [HttpGet("TweetReplies/{id}")]
        public ActionResult<IEnumerable<Tweet>> GetTweetReplies(int id)
        {
            return this._tweetService.GetTweetReplies(id).ToList();
        }

        // DELETE: api/Tweets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            if(await _tweetService.DeleteTweet(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        private bool TweetExists(int id)
        {
            return _tweetService.TweetExists(id);
        }
    }
}
