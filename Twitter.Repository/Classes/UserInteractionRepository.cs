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
    public class UserInteractionRepository : Repository<Following>, IUserInteractionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserInteractionRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task Follow(Following following)
        {
            //Insert(following);
            _context.Following.Add(following);
            await _context.SaveChangesAsync();

           
        }

        
        public async Task UnFollow(Following following)
        {
            _context.Following.Remove(following);
            await _context.SaveChangesAsync();
        }


        public List<ApplicationUser> GetFollowers(string userId)
        {
          
            return _context.Following.Where(u => u.FollowerId == userId).Select(u => u.FollowingUser).ToList();
            
        }


    }
}
