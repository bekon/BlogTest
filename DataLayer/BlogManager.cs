using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GenericRepository.EF;
using GenericRepository;


namespace DataLayer
{
    public class BlogManager : Repository<EntryContext, Entry>
    {
        private EntryContext _entry;

        public BlogManager()
        {
            _entry = new EntryContext();
            Database sb =_entry.Database;
        }

        /// <summary>
        /// returns count of blog entries
        /// </summary>
        public int Count
        {
            get
            {
                return _entry.Entries.Count();
            }
        }

        /// <summary>
        /// returns count of all user's post
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetCountForUser(string userName)
        {
            int count;
            count = _entry.Entries.Count(post => post.Author == userName);
            return count;
        }

        /// <summary>
        /// Gets blog entry 
        /// </summary>
        /// <param name="id">post id</param>
        /// <returns></returns>
        public Entry GetEntry(int id)
        {
            return _entry.Entries.FirstOrDefault(item => item.Id == id);
        }

        /// <summary>
        /// returns all blog entries
        /// </summary>
        /// <returns></returns>
        public IQueryable<Entry> GetEntries()
        {
            return _entry.Entries.OrderByDescending(en => en.Created);
        }

        /// <summary>
        /// returns blog entries
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IQueryable<Entry> GetEntries(int page, int pageSize = 5)
        {
            return _entry.Entries.OrderByDescending(en => en.Created).Skip(pageSize * (page - 1)).Take(pageSize);
        }

        /// <summary>
        /// returns user's entries
        /// </summary>
        /// <param name="userName">name of author</param>
        /// <param name="page">page number</param>
        /// <returns></returns>
        public IQueryable<Entry> GetEntries(string userName, int page, int pageSize = 5)
        {
            return _entry.Entries.Where(post => post.Author == userName).OrderByDescending(en => en.Created).Skip(pageSize * (page - 1)).Take(pageSize);
        }

        /// <summary>
        /// updates entry in database
        /// </summary>
        /// <param name="obj">blog entry</param>
        public void SaveEntry(Entry obj)
        {
            Entry old = GetEntry(obj.Id);
            old.Title = obj.Title;
            old.Content = obj.Content;
            _entry.SaveChanges();
        }

    }
}
