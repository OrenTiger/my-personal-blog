using System.Web.Mvc;

namespace MyPersonalBlog.Infrastructure
{
    public static partial class Utilities
    {
        /// <summary>
        /// HTML-хэлпер, проверяющий активный контроллер и метод
        /// </summary>
        /// <param name="action">Имя метода</param>
        /// <param name="controller">Имя контроллера</param>
        /// <returns>Возвращает пустую строку или "active"</returns>
        public static string IsActive(this HtmlHelper html, string action, string controller)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            var returnActive = action == routeAction && controller == routeControl; 

            return returnActive ? "active" : "";
        }
    }
}