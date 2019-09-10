using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Category
{
    public class CategoryController : Controller
    {
        public readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public ActionResult Create(CategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException(this.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
            }

            this.categoryService.Create(model);

            return this.Redirect(this.Url.Action("All", "Product"));
        }

        public ActionResult GetCategoryProducts(string id)
        {
            CategoryViewModel category = this.categoryService.GetCategory(id);

            if (category == null)
            {
                throw new InvalidOperationException("Invalid Category id.");
            }

            return this.Json(category.Products, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Delete(string id)
        {
            await this.categoryService.Delete(id);
        }

        [Authorize]
        public ActionResult Get(string id)
        {
            CategoryViewModel categoryModel = this.categoryService.GetCategory(id);
                
            return this.Json(categoryModel, JsonRequestBehavior.AllowGet);
        }
    }
}