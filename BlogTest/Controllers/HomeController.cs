using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BlogTest.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AutoMapper;
using NLog;
using BlogTest.Filters;

namespace BlogTest.Controllers
{
    [Authorize]
    [BlogoHandleError]
    public class HomeController : Controller
    {
        /// <summary>
        /// Shows blog entries 
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Amount of entries on page</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            BlogManager db = new BlogManager();
            int count = db.Count;
            IQueryable iq = db.GetAll();
            ViewBag.PagesCount = Math.Ceiling((float)count / (float)pageSize);
            return View();
        }

        /// <summary>
        /// Shows posts of user
        /// </summary>
        /// <param name="user">User name</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Amount of entries on page</param>
        /// <returns></returns>
        public ActionResult UserPosts(string user, int page = 1, int pageSize = 5)
        {
            BlogManager db = new BlogManager();
            int count = db.GetCountForUser(user);
            if (count == 0)
            {
                return View("UserHaveNoPosts");
            }
            ViewBag.PagesCount = Math.Ceiling((float)count / (float)pageSize);
            return View();
        }

        /// <summary>
        /// Gets entries for current page
        /// </summary>
        /// <param name="page">current page</param>
        /// <param name="user">user name</param>
        /// <returns></returns>
        public ActionResult GetPage(int page, string user = "all")
        {
            BlogManager db = new BlogManager();
            IQueryable<Entry> items;
            EntryModel[] et;
            if (user == "all")
            {
                items = db.GetEntries(page);
                Entry[] entries = items.ToArray();
                et = Mapper.Map<Entry[], EntryModel[]>(entries);
            }
            else
            {
                user = Server.UrlDecode(user);
                items = db.GetEntries(user, page);
                et = Mapper.Map<EntryModel[]>(items.ToArray());
            }
            ViewBag.Items = et;
            return View(et);
        }

        //About
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Shows blog entry
        /// </summary>
        /// <param name="id">Id of entry</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewEntry(int id)
        {
            BlogManager db = new BlogManager();
            Entry ent = db.GetEntry(id);
            EntryModel entry = Mapper.Map<EntryModel>(ent);
            if (ent != null)
            {
                return View(entry);
            }
            else
            {
                return View("PostNotFound");
            }
        }

        /// <summary>
        /// Edits blog entry
        /// </summary>
        /// <param name="id">Entry id</param>
        /// <returns>Edit form for blog entry</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!User.IsInRole("chief") && !User.IsInRole("writer"))
            {
                return Redirect("~/Home/AccessError");
            }
            BlogManager db = new BlogManager();
            Entry ent = db.GetEntry(id);
            if (ent == null)
            {
                return Redirect("~/Home/Index");
            }
            if (User.IsInRole("writer") && User.Identity.Name != ent.Author)
            {
                return Redirect("~/Home/AccessError");
            }
            return View(ent);
        }

        /// <summary>
        /// Edits blog entry
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Entry ent)
        {
            if (!User.IsInRole("chief") && !User.IsInRole("writer"))
            {
                return Redirect("~/Home/AccessError");
            }
            if (User.IsInRole("writer") && User.Identity.Name != ent.Author)
            {
                return Redirect("~/Home/AccessError");
            }
            BlogManager db = new BlogManager();
            db.SaveEntry(ent);
            return RedirectToAction("ViewEntry", new { id = ent.Id });
        }

        /// <summary>
        /// Delete entry
        /// </summary>
        /// <param name="id">entry id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            BlogManager db = new BlogManager();
            Entry etr = db.GetEntry(id);
            if ((etr.Author == User.Identity.Name && User.IsInRole("writer"))
                || User.IsInRole("chief"))
            {
                db.Delete(etr);
                db.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect("~/Home/AccessError");
            }
        }

        /// <summary>
        /// Shows form for add entry
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            if (!User.IsInRole("writer") && !User.IsInRole("chief"))
            {
                return Redirect("~/Home/AccessError");
            }
            return View();
        }

        /// <summary>
        /// Adds entry to the blog
        /// </summary>
        /// <param name="entry">Entry</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(EntryModel entry)
        {
            if (!User.IsInRole("writer") && !User.IsInRole("chief"))
            {
                return Redirect("~/Home/AccessError");
            }
            if (ModelState.IsValid)
            {
                Entry newEntry;
                BlogManager db = new BlogManager();
                entry.Created = DateTime.Now;
                entry.Author = User.Identity.Name;
                newEntry = Mapper.Map<Entry>(entry);
                db.Add(newEntry);
                db.Save();
                return RedirectToAction("ViewEntry", new { id = newEntry.Id });
            }
            return View(entry);
        }

        /// <summary>
        /// Shows view with list of bloggers
        /// </summary>
        /// <returns></returns>
        public ActionResult Bloggers()
        {
            return View();
        }

        /// <summary>
        /// Gets list of bloggers for specified request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of bloggers in JSON format</returns>
        public ActionResult GetBloggers([DataSourceRequest] DataSourceRequest request)
        {
            int total;
            MembershipUserCollection users = Membership.GetAllUsers(request.Page - 1, request.PageSize, out total);
            List<Models.BloggerModel> lst = new List<BloggerModel>(Mapper.Map<Models.BloggerModel[]>(users));
            request.Page = 1;
            DataSourceResult src = lst.ToDataSourceResult(request);
            src.Total = total;
            return Json(src, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Shows error when access denied
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessError()
        {
            return View();
        }

        /// <summary>
        /// Contact info
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }
    }
}
