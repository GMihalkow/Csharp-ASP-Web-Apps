using ShopApp.Api.Infrastructure.Filters;
using System.Web.Http.Filters;

namespace ShopApp.Api.App_Start
{
    public class FilterConfig
    {
        public static void RegisterFilters(HttpFilterCollection filters)
        {
            filters.Clear();
            filters.Add(new LogExceptionFilter());
            filters.Add(new LogRequestFIlter());
        }
    }
}