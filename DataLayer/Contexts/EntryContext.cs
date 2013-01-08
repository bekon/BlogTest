using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Contexts
{
        public class EntryContext : DbContext
        {
            public EntryContext()
                : base("name=DefaultConnection")
            {

            }
            public DbSet<Entry> Entries { get; set; }
        }
}
