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
                url: "{controller}/{id}",
                defaults: new { controller = "GetPosts", action = "View" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Manage/Login/{action}",
                defaults: new { controller = "Login", action = "SignIn" }
            );

            routes.MapRoute(
                name: "Manage",
                url: "Manage/Admin/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Posts", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "(?!Admin).*" }
            );
        }
    }
}
