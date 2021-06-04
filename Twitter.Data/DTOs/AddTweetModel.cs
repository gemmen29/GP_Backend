using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Models;

namespace Twitter.Data.DTOs
{
    public class AddTweetModel
    {
        public string Body { get; set; }
        public List<Image> Images { get; set; }
        public Video Video { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorId { get; set; }
    }
}
