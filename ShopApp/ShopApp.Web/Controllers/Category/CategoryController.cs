using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Category
{
    public class CategoryController : BaseController
    {
        public readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Edit(CategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Something went wrong");
            }

            await this.categoryService.Edit(model);

            return this.RedirectToAction("All", "Product");
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Create(CategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException(this.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
            }

            await this.categoryService.Create(model);

            return this.RedirectToAction("All", "Product");
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Delete(string id)
        {
            await this.categoryService.Delete(id);

            return this.RedirectToAction("All", "Product");
        }
    }
}