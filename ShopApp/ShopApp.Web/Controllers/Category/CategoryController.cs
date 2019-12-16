using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Web.Constants;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers
{
    public class CategoryController : BaseController
    {
        public readonly ICategoryService categoryService;
        private readonly IRepository<CategoryViewModel, CategoryInputModel> categoryRepository;

        public CategoryController(ICategoryService categoryService, IRepository<CategoryViewModel, CategoryInputModel> categoryRepository)
        {
            this.categoryService = categoryService;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Edit(CategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Something went wrong");
            }

            await this.categoryRepository.Edit(model);

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

            await this.categoryRepository.Create(model);

            return this.RedirectToAction("All", "Product");
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Delete(string id)
        {
            await this.categoryRepository.Delete(id);

            return this.RedirectToAction("All", "Product");
        }
    }
}