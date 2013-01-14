using System.Collections.Generic;

namespace BlogTest.Models.Kendo
{
    public class KendoGridRequest
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public IEnumerable<KendoSort> sort { get; set; }
    }
}