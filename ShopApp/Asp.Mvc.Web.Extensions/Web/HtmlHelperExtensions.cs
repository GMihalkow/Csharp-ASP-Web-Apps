using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Asp.Mvc.Web.Extensions.Web
{
    public static class HtmlHelperExtensions
    {
        public static string CurrentController(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }

        public static MvcHtmlString ActionLinkWithoutArea(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object htmlAttributes = default(object))
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new { area = string.Empty }, htmlAttributes);
        }
    }
}