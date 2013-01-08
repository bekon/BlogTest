using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlogTest.Models
{
    public class EntryModel
    {
        public int Id { get; set; }
        [MaxLength(90)]
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}