using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Service.Classes;

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private SearchUserService _searchUserService;
        public SearchController(SearchUserService searchUserService)
        {
            _searchUserService = searchUserService;
        }
        [HttpGet("count")]
        public IActionResult NumberOfUsers()
        {
            return Ok(_searchUserService.CountEntity());
        }
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetUsersByPage(int pageSize, int pageNumber)
        {
            return Ok(_searchUserService.GetPageRecords(pageSize, pageNumber));
        }
        [HttpGet("count/{keyword}")]
        public IActionResult NumberOfUsersPerKeyword(string keyword)
        {
            return Ok(_searchUserService.CountEntityByKeyword(keyword));
        }
        [HttpPost("search")]
        public IActionResult GetUsersByPage(SearchModel searchModel)
        {
            return Ok(_searchUserService.GetPageByKeywords(searchModel));
        }
    }
}
