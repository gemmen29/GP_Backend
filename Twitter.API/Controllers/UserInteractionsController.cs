using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;
using Twitter.Service.Interfaces;

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInteractionsController : ControllerBase
    {
        private readonly IUserInteractionService _userInteractionService ;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInteractionsController(IUserInteractionService userInteractionService, IHttpContextAccessor httpContextAccessor)
        {
            _userInteractionService = userInteractionService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("/user/follow/{followingId}")]
        public IActionResult Follow(string followingId)
        {
            var userID = "189180df-f7f4-4782-947a-351dd83d0668";

            //var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _userInteractionService.Follow(new Following() { FollowerId = userID, FollowingId = followingId });

            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id, addTweetModel);
            return NoContent();
        }

        [HttpPost("/user/unfollow/{followingId}")]
        public IActionResult UnFollow(string followingId)
        {
            var userID = "189180df-f7f4-4782-947a-351dd83d0668";

            //var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _userInteractionService.UnFollow(new Following() { FollowerId = userID, FollowingId = followingId });

            //return CreatedAtAction("GetTweet", new { id = addTweetModel.Id, addTweetModel);
            return NoContent();
        }


        [HttpGet("/user/following")]
        public ActionResult<IEnumerable<FollowingDetails>> GetFollowing()
        {
            //var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userID = "189180df-f7f4-4782-947a-351dd83d0668";
            return _userInteractionService.GetFollowing(userID).ToList();
        }



    }
}
