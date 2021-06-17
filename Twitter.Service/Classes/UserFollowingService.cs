using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;
using Twitter.Repository.Interfaces;
using Twitter.Service.Interfaces;

namespace Twitter.Service.Classes
{
    public class UserFollowingService : BaseService, IUserFollowingService
    {
        private IUserFollowingRepository  _userFollowingRepository { get; }

        public UserFollowingService(IUserFollowingRepository userFollowingRepository, IMapper mapper) : base(mapper)
        {
            _userFollowingRepository = userFollowingRepository;
        }
        public void Follow(Following following)
        {
            _userFollowingRepository.Follow(following);
        }

        public void UnFollow(Following following)
        {
            _userFollowingRepository.UnFollow(following);
        }

        public List<UserInteractionDetails> GetFollowing(int pageSize, int pageNumber, string userId)
        {
            return Mapper.Map<UserInteractionDetails[]>(_userFollowingRepository.GetFollowings(pageSize, pageNumber, userId)).ToList();
        }

        public bool FollowingExists(string userId, string followingId)
        {
            return _userFollowingRepository.FollowingExists(userId, followingId);
        }

        public List<UserInteractionDetails> GetFollowers(int pageSize, int pageNumber, string userId)
        {
            return Mapper.Map<UserInteractionDetails[]>(_userFollowingRepository.GetFollowers(pageSize, pageNumber, userId)).ToList();
        }

        public IEnumerable<UserDetails> SuggestedFollowers(string userId)
        {
            return Mapper.Map<UserDetails[]>(_userFollowingRepository.SuggestedFollowers(userId)).ToList(); ;
        }
    }
}
