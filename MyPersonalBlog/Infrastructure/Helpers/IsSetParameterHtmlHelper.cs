using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Linq;

namespace MyPersonalBlog.Infrastructure
{
    public static partial class Utilities
    {
        /// <summary>
        /// HTML-хэлпер, проверяющий, установлен/не установлен ли параметр в QueryString
        /// </summary>
        /// <param name="paramName">Имя проверяемого параметра</param>
        /// <param name="checkedValue">Проверяемое значение параметра (пустая строка - параметр не установлен)</param>
        /// <param name="successString">Возвращаемая строка (в случае успеха)</param>
        /// <returns>Возвращает successString в случае успеха, иначе пустую строку</returns>
        public static string IsSetParameter(this HtmlHelper html, string paramName, string checkedValue, string successString)
        {
            var queryString = html.ViewContext.RequestContext.HttpContext.Request.QueryString;

            if (string.IsNullOrEmpty(checkedValue))
            {
                if (string.IsNullOrEmpty(queryString[paramName]))
                    return successString;
            }
            else
            {
                if (queryString[paramName] == checkedValue)
                    return successString;
            }

            return string.Empty;
        }
    }
}