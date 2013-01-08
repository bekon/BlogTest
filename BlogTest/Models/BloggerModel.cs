using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTest.Models
{
    public class BloggerModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}