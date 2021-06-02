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
    public class UserInteractionService : BaseService, IUserInteractionService
    {
        private IUserInteractionRepository  _userInteractionRepository { get; }

        public UserInteractionService(IUserInteractionRepository userInteractionRepository, IMapper mapper) : base(mapper)
        {
            _userInteractionRepository = userInteractionRepository;
        }
        public void Follow(Following following)
        {

            _userInteractionRepository.Follow(following);
           
        }

        public void UnFollow(Following following)
        {
            _userInteractionRepository.UnFollow(following);

        }

        public List<FollowingDetails> GetFollowing(string userId)
        {
            return Mapper.Map<FollowingDetails[]>(_userInteractionRepository.GetFollowers(userId)).ToList();
        }
    }
}
