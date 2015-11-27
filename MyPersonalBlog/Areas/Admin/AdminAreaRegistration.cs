using System.Web.Mvc;

namespace MyPersonalBlog.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                null,
                "Manage/Login/{action}",
                new { controller = "Login", action = "SignIn" }
            );

            context.MapRoute(
                "Admin_default",
                "Manage/{controller}/{action}/{id}",
                new { action = "List", id = UrlParameter.Optional },
                new string[] { "MyPersonalBlog.Areas.Admin.Controllers" }
            );
        }
    }
}