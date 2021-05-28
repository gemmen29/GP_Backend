using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Models;

namespace Twitter.Service.Interfaces
{
    public interface ITweetService
    {
        IEnumerable<Tweet> GetTweets();
        Tweet GetTweet(int id);
        Task<Tweet> PostTweet(string authorId, Tweet tweet);
        Task<Tweet> PostReplyToTweet(int id, string authorId, Tweet tweet);
        IEnumerable<Tweet> GetTweetReplies(int id);
        Task<bool> DeleteTweet(int id);
        bool TweetExists(int id);
    }
}
