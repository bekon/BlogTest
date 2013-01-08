using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer
{
    public class EntryContext : DbContext
    {
        public EntryContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    public class Entry
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(90)]
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
