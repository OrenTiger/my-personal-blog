using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyPersonalBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "page{page}",
                defaults: new { controller = "Posts", action = "Index" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: null,
                url: "Search/{query}",
                defaults: new { controller = "Search", action = "Search", query = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: null,
                url: "Manage/Login/{action}",
                defaults: new { controller = "Login", action = "SignIn" }
            );

            routes.MapRoute(
                name: null,
                url: "Manage/Admin/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Posts", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "(?!Admin).*" }
            );
        }
    }
}
