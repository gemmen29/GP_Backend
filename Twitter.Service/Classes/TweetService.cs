using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Models;
using Twitter.Repository;
using Twitter.Repository.classes;
using Twitter.Service.Interfaces;

namespace Twitter.Service.Classes
{
    public class TweetService : Repository<Tweet> , ITweetService 
    {
        private readonly ApplicationDbContext _context;

        public TweetService(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> DeleteTweet(int id)
        {
            var tweet = GetById(id);
            if (tweet == null)
            {
                return false;
            }

            Delete(id);
            await _context.SaveChangesAsync();

            return true;
        }

        public Tweet GetTweet(int id)
        {
            return _context.Tweet.Where(t => t.Id == id).Include(t => t.Author).FirstOrDefault();
        }

        public IEnumerable<Tweet> GetTweetReplies(int id)
        {
            var replies = _context.Reply.Where(r => r.TweetId == id).ToList();
            List<Tweet> tweets = new List<Tweet>();

            foreach (var reply in replies)
            {
                var tweet = _context.Tweet.Where(t => t.Id == reply.ReplyId).Include(t => t.Author).FirstOrDefault();
                tweets.Add(tweet);
            }
            return tweets;
        }

        public IEnumerable<Tweet> GetTweets()
        {
            return _context.Tweet.Include(t => t.Author);
        }

        public IEnumerable<Tweet> GetMyTweets(string id)
        {
            ApplicationUser author = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            return _context.Tweet.Where(t => t.Author == author);
        }

        public IEnumerable<Tweet> GetHomePageTweets(string id)
        {
            var followings = _context.Following.Where(f => f.FollowerId == id).Select(f => f.FollowingUser).ToList();
            var tweets = new List<Tweet>();
            
            foreach(var following in followings)
            {
                tweets.AddRange(_context.Tweet.Where(t => t.Author == following).ToList());
            }
            
            ApplicationUser author = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            tweets.AddRange(_context.Tweet.Where(t => t.Author == author).ToList());

            return tweets;
        }

        public async Task<Tweet> PostReplyToTweet(int id, string authorId, Tweet tweet)
        {
            ApplicationUser author = _context.Users.Where(u => u.Id == authorId).FirstOrDefault();
            tweet.Author = author;
            Insert(tweet);
            await _context.SaveChangesAsync();

            Reply reply = new Reply() { TweetId = id, ReplyId = tweet.Id };
            _context.Reply.Add(reply);
            await _context.SaveChangesAsync();

            return tweet;
        }

        public async Task<Tweet> PostTweet(string authorId, Tweet tweet)
        {
            ApplicationUser author = _context.Users.Where(u => u.Id == authorId).FirstOrDefault();
            tweet.Author = author;
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
    }
}
