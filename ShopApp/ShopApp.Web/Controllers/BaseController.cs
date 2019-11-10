using Microsoft.AspNet.Identity.Owin;
using ShopApp.Web.Utilities;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public string LoggedUserId
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    var userManager = this.HttpContext.GetOwinContext().GetUserManager<ShopUserManager>();

                    return userManager.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name).Id;
                }

                return null;
            }
        }
    }
}