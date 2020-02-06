using Microsoft.AspNetCore.Mvc;
using ShopApp.Dal.Services.User.Contracts;

namespace ShopApp.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string LoggedUserId
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    var userService = this.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;

                    var currentUserId = userService.GetUserByName(this.User.Identity.Name)?.Id;

                    return currentUserId;
                }
                
                return default(string);
            }
        }
    }
}