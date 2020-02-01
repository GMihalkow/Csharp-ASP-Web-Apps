using Microsoft.AspNetCore.Mvc;

namespace ShopApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}