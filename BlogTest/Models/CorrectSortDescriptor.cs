using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Kendo.Mvc.UI;

namespace BlogTest.Models
{
    public class CorrectSortDescriptor
    {
        public ListSortDirection SortDirection { get; set; }
        public string Member { get; set; }
        public void Deserialize(string source)
        {
            string str = source;
            char[] chArray = new char[1];
            int index = 0;
            int num = 45;
            chArray[index] = (char)num;
            string[] strArray = str.Split(chArray);
            if (strArray.Length > 1)
                this.Member = strArray[0];
            this.SortDirection = !(Enumerable.Last<string>((IEnumerable<string>)strArray) == "desc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
        }
    }
}