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
    public class UserFollowingRepository : Repository<Following>, IUserFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public UserFollowingRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public void Follow(Following following)
        {
            Insert(following);
            Commit();
            //_context.Following.Add(following);
            //await _context.SaveChangesAsync(); 
        }

        public void UnFollow(Following following)
        {
            //_context.Following.Remove(following);
            //await _context.SaveChangesAsync();
            Delete(following);
            Commit();
        }

        public IEnumerable<ApplicationUser> GetFollowers(int pageSize, int pageNumber, string userId)
        {
            //return _context.Following.Where(u => u.FollowingId == userId).Select(u => u.FollowerUser).ToList();
            return GetPageRecordsWhere(pageSize, pageNumber, u => u.FollowingId == userId, "FollowerUser").Select(u => u.FollowerUser).ToList();
        }

        public bool FollowingExists(string userId, string followingId)
        {
            return GetFirstOrDefault(f => f.FollowerId == userId && f.FollowingId == followingId) != null ? true : false;
        }

        public IEnumerable<ApplicationUser> GetFollowings(int pageSize, int pageNumber, string userId)
        {
            //return _context.Following.Where(u => u.FollowerId == userId).Select(u => u.FollowingUser).ToList(); 
            return GetPageRecordsWhere(pageSize, pageNumber, u => u.FollowerId == userId, "FollowingUser").Select(u => u.FollowingUser).ToList();
        }
    }
}
