using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using SimpleShop.Web.Models;
using System;

namespace SimpleShop.Web.Filters.Exception
{
    public class ErrorViewFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var model = new ErrorViewModel { Message = "TEST" };

            var result = new RedirectToRouteResult(new RouteValueDictionary
                       {
                           { "action", "Error" },
                           { "controller", "Home" },
                           { "message", context.Exception.Message }
                       });
            

            context.Result = result;
        }
    }
}