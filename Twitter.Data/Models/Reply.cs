using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Data.Models
{
    public class Reply
    {
        public int Id { get; set; }
        List<Tweet> Tweets { get; set; }
    }
}
