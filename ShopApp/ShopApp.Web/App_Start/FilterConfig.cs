﻿using ShopApp.Web.Infrastructure.Filters;
using System.Web.Mvc;

namespace ShopApp.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogExceptionFilter());
        }
    }
}