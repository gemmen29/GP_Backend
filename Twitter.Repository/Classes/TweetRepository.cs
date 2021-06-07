using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Models;
using Twitter.Repository.classes;
using Twitter.Repository.Interfaces;

namespace Twitter.Repository.Classes
{
    public class TweetRepository : Repository<Tweet>,  ITweetRepository
    {
        private readonly ApplicationDbContext _context;

        public TweetRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
       
        public Tweet GetTweet(int id)
        {
            return _context.Tweet.Where(t => t.Id == id).Include(t => t.Author).Include(t => t.Images).Include(t => t.Video).Include(t => t.Replies).ThenInclude(r => r.Tweet).FirstOrDefault();
        }

        public IEnumerable<Tweet> GetTweetReplies(int id)
        {
            var replies = _context.Reply.Where(r => r.TweetId == id).ToList();
            List<Tweet> tweets = new List<Tweet>();

            foreach (var reply in replies)
            {
                var tweet = _context.Tweet.Where(t => t.Id == reply.ReplyId).Include(t => t.Author).Include(t => t.Images).Include(t => t.Video).FirstOrDefault();
                tweets.Add(tweet);
            }
            return tweets;
        }

        public IEnumerable<Tweet> GetTweets(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return _context.Tweet.Skip(pageNumber * pageSize).Take(pageSize).Include(t => t.Author).Include(t => t.Images).Include(t => t.Video).ToList();
        }

        public int GetTweetsCount()
        {
            return CountEntity();
        }

        public IEnumerable<Tweet> GetMyTweets(string id, int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return _context.Tweet.Where(t => t.AuthorId == id).Skip(pageNumber * pageSize).Take(pageSize).Include(t=>t.Author).Include(t => t.Images).Include(t => t.Video).ToList();
        }

        public IEnumerable<Tweet> GetHomePageTweets(string id, int pageSize, int pageNumber)
        {
            var followingIds = _context.Following.Where(f => f.FollowerId == id).Select(f => f.FollowingUser.Id).ToList();
            ApplicationUser author = _context.Users.FirstOrDefault(u => u.Id == id);
            
            followingIds.Add(author.Id);

            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return 
                _context.Tweet.Where(t => followingIds.Contains(t.AuthorId))
                .OrderByDescending(t => t.CreationDate)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .Include(t => t.Author).Include(t => t.Replies).Include(t => t.LikedTweets).Include(t => t.Images).Include(t => t.Video)
                .ToList();
            
        }

        public async Task<Tweet> PostReplyToTweet(int id, Tweet tweet)
        {
            Insert(tweet);
            await _context.SaveChangesAsync();

            Reply reply = new Reply() { TweetId = id, ReplyId = tweet.Id };
            _context.Reply.Add(reply);
            await _context.SaveChangesAsync();

            return tweet;
        }

        public async Task<Tweet> PostTweet(Tweet tweet)
        {
            //await _context.BulkInsertAsync(new List<Tweet>() { tweet});
            Insert(tweet);
            await _context.SaveChangesAsync();

            return tweet;
        }

        public bool TweetExists(int id)
        {
            Tweet tweet = GetById(id);
            if (tweet == null)
                return false;
            else
                return true;
        }

        public async Task DeleteTweet(int id)
        {
            Delete(id);
            await _context.SaveChangesAsync();
        }
    }
}
