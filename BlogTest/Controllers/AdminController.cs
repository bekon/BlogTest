using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlogTest.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BlogTest.Filters;

namespace BlogTest.Controllers
{
    [Authorize]
    [BlogoHandleError]
    public class AdminController : Controller
    {
        /// <summary>
        /// Shows users and their's roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!User.IsInRole("chief"))
                return Redirect("~/Home/AccessError");
            return View("Users");
        }

        /// <summary>
        /// Returns list of users with server paging
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetUsers(GridRequestModel request)
        {
            int total;
            MembershipUserCollection users = Membership.GetAllUsers(request.Page - 1, request.PageSize, out total);
            List<UserModel> lst = new List<UserModel>();
            foreach (MembershipUser item in users)
            {
                UserModel usrMod = new UserModel();
                usrMod.UserName = item.UserName;
                string[] roles = Roles.GetRolesForUser(item.UserName);
                usrMod.Role = roles[0];
                lst.Add(usrMod);
            }
            request.Page = 1;
            DataSourceResult res = lst.ToDataSourceResult(request);
            res.Total = total;
            ViewBag.PagesCount = Math.Ceiling((float)total / (float)request.PageSize);
            ViewBag.Roles = MvcApplication.UserRoles;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sets new role to user
        /// </summary>
        /// <param name="userName">Name of user</param>
        /// <param name="newRole">New role to set</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeRole(string userName, string newRole)
        {
            if (!User.IsInRole("chief"))
                return Redirect("~/Home/AccessError");
            if (Roles.RoleExists(newRole))
            {
                Roles.RemoveUserFromRole(userName, Roles.GetRolesForUser(userName)[0]);
                Roles.AddUserToRole(userName, newRole);
                return new EmptyResult();
            }
            else
            {
                return Redirect("There will be an error :)");
            }
        }
  
    }
}
