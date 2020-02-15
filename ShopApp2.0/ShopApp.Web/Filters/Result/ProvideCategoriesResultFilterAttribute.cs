using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopApp.Dal.Services.Category.Contracts;

namespace ShopApp.Web.Filters.Result
{
    public class ProvideCategoriesResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.HttpContext.RequestServices.GetService(typeof(ICategoryService)) is ICategoryService
                categoryService)
            {
                context.HttpContext.Items["categories"] = await categoryService.GetCategoriesForDropdown();
                
                await base.OnResultExecutionAsync(context, next);
            }
            else
            {
                throw new InvalidOperationException("Category Service not found.");
            }
        }
    }
}