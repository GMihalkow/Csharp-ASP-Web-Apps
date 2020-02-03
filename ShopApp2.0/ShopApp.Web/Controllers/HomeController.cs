using Microsoft.AspNetCore.Mvc;

namespace ShopApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}