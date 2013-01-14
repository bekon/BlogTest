using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTest.Models.Kendo
{
    public class KendoGridResult
    {
        public int Total { get; set; }
        public IEnumerable<object> Data { get; set; }
    }
}