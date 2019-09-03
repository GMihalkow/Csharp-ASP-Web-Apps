using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}