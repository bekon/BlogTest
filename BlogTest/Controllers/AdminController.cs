using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlogTest.Models;
using BlogTest.Models.Kendo;
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
        public ActionResult GetUsers(KendoGridRequest request)
        {
            int total;
            var users = Membership.GetAllUsers(request.page - 1, request.pageSize, out total);
            var usersList = new List<UserModel>();
            foreach (MembershipUser item in users)
            {
                var userModel = new UserModel {UserName = item.UserName};
                var roles = Roles.GetRolesForUser(item.UserName);
                if (roles.Length > 0)
                {
                    userModel.Role = roles[0];
                }
                usersList.Add(userModel);
            }
            return Json(new KendoGridResult {Data = usersList, Total = total});
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
