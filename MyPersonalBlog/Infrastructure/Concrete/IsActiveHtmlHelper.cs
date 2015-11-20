using System.Web.Mvc;

namespace MyPersonalBlog.Infrastructure
{
    public static partial class Utilities
    {
        public static string IsActive(this HtmlHelper html, string controller, string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            var returnActive = controller == routeControl && action == routeAction;

            return returnActive ? "active" : "";
        }
    }
}