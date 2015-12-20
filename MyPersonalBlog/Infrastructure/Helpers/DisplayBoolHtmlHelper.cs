using System.Text;
using System.Web.Mvc;

namespace MyPersonalBlog.Infrastructure
{
    public static partial class Utilities
    {
        /// <summary>
        /// HTML-хэлпер для отображения логического значения в виде "Да/Нет"
        /// </summary>
        /// <param name="value">Логическое значение</param>
        /// <returns>Возвращает элемент <span></returns>
        public static MvcHtmlString DisplayBool(this HtmlHelper html, bool value)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder tag = new TagBuilder("span");

            if (value == true)
            {
                tag.InnerHtml = "Да";
                tag.AddCssClass("text-success");
            }
            else
            {
                tag.InnerHtml = "Нет";
                tag.AddCssClass("text-warning");
            }

            result.Append(tag.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}

