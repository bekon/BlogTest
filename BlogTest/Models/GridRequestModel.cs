using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.UI;

namespace BlogTest.Models
{
    public class GridRequestModel : DataSourceRequest
    {
        public IList<CorrectSortDescriptor> Sort { get; set; }
    }
}