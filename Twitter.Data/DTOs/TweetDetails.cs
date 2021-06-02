using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Data.DTOs
{
    public class TweetDetails
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public int LikeCount { get; set; }
        public int ReplyCount { get; set; }
        public UserDetails Author { get; set; }

    }
}
