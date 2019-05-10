using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SimpleShop.Web.Filters.Exception
{
    public class ErrorViewFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //TODO: Finish the exception filter
            var result = new ViewResult { ViewName = "Error" };
            result.ViewData = new ViewDataDictionary(null, context.ModelState);
            result.ViewData.Add("Message", context.Exception);

            context.Result = result;
        }
    }
}