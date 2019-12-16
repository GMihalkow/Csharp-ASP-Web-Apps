using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository<CategoryViewModel, CategoryInputModel> categoryRepository;

        public HomeController(ICategoryService categoryService, IRepository<CategoryViewModel, CategoryInputModel> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            List<CategoryViewModel> categories = this.categoryRepository.GetAll().Take(3).ToList();

            return this.View(categories);
        }
    }
}