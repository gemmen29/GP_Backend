using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Models;

namespace Twitter.Repository.Interfaces
{
    public interface IUserInteractionRepository
    {
        public Task Follow(Following following);
        public Task UnFollow(Following following);
        public List<ApplicationUser> GetFollowers(string userId); 

    }
}
