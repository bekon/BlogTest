using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using System.Web.Security;
using AutoMapper;

namespace BlogTest
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static string[] UserRoles = { "reader", "writer", "chief" };
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            RegisterRoles();
            CreateMaps();
        }

        //check and register roles
        protected void RegisterRoles()
        {
            if (!Roles.RoleExists("reader"))
                Roles.CreateRole("reader");
            if (!Roles.RoleExists("writer"))
                Roles.CreateRole("writer");
            if (!Roles.RoleExists("chief"))
                Roles.CreateRole("chief");
        }

        protected void CreateMaps()
        {
            Mapper.CreateMap<DataLayer.Entry, BlogTest.Models.EntryModel>();
            Mapper.CreateMap<BlogTest.Models.EntryModel, DataLayer.Entry>();
            Mapper.CreateMap<MembershipUser, BlogTest.Models.BloggerModel>();
        }
    }
}