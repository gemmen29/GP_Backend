using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;

namespace Twitter.Service.Interfaces
{
    public interface IUserFollowingService
    {
        public void Follow(Following following);
        public void UnFollow(Following following);
        public List<UserInteractionDetails> GetFollowing(int pageSize, int pageNumber, string userId);
        public List<UserInteractionDetails> GetFollowers(int pageSize, int pageNumber, string userId);
        public bool FollowingExists(string userId, string followingId);
        public IEnumerable<UserDetails> SuggestedFollowers(string userId);
    }
}
