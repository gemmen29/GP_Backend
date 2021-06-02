using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;

namespace Twitter.Service.Interfaces
{
    public interface IUserInteractionService
    {
        public void Follow(Following following);
        public void UnFollow(Following following);
        public List<FollowingDetails> GetFollowing(string userId);

    }
}
