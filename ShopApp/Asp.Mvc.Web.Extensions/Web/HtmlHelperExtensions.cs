using System.Web.Mvc;

namespace Asp.Mvc.Web.Extensions.Web
{
    public static class HtmlHelperExtensions
    {
        public static string CurrentController(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }
    }
}