using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            List<CategoryViewModel> categories = this.categoryService.GetCategories().Take(3).ToList();

            return this.View(categories);
        }
    }
}