using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Data.DTOs
{
    public class AddTweetModel
    {
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorId { get; set; }
    }
}
