using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Data.DTOs;

namespace Twitter.API.HubConfig
{
    public class TweetHub : Hub
    {
        public async Task BroadcastTweetList(List<TweetDetails> tweetDetails)
        {
            await Clients.All.SendAsync("BroadcastTweetList", tweetDetails);
        }
    }
}
