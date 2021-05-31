using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Twitter.Data.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public List<UserLikes> LikedTweets { get; set; }

        [JsonIgnore]
        public List<UserBookmarks> BookMarkedTweets { get; set; }

        public string AuthorId { get; set; }
        [JsonIgnore, InverseProperty("Tweets"), ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        [JsonIgnore]
        public List<Reply> Replies { get; set; }

        [JsonIgnore]
        public Reply RespondedTweet { get; set; }
    }
}
