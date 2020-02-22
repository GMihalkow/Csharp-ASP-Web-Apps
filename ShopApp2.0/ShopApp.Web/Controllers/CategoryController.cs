using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Web.Constants;
using ShopApp.Web.Extensions;
using ShopApp.Web.Models;

namespace ShopApp.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IRepository<CategoryViewModel, CategoryBaseInputModel> _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryController(IRepository<CategoryViewModel, CategoryBaseInputModel> categoryRepository, ICategoryService categoryService)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> Create(CategoryBaseInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                inputModel.CreatorId = this.LoggedUserId;

                await this._categoryRepository.Create(inputModel);

                this.TempData.AddSerialized<Alert>("Alerts", new Alert(AlertType.Success, "Successfully created category."));
                
                return this.RedirectToAction(nameof(this.All));
            }
            catch (ArgumentException e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);

                return this.View(inputModel);
            }
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public IActionResult Edit(string id)
        {
            try
            {
                var categoryModel = this._categoryRepository.Get(id);

                var categoryEditModel = new CategoryEditInputModel
                {
                    Id = categoryModel.Id,
                    Name = categoryModel.Name,
                    CoverUrl = categoryModel.CoverUrl
                };

                return this.View(categoryEditModel);
            }
            catch (ArgumentException)
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> Edit(CategoryEditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this._categoryRepository.Edit(inputModel);

                this.TempData.AddSerialized<Alert>("Alerts", new Alert(AlertType.Success, "Successfully edited category."));
                
                return this.RedirectToAction(nameof(this.All));
            }
            catch (ArgumentException e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);

                return this.View(inputModel);
            }
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> All()
        {
            var categories = await this._categoryService.GetCategoriesForTable();

            if (categories.IsNullOrEmpty())
            {
                this.TempData.AddSerialized<Alert>("Alerts", new Alert(AlertType.Info, "No categories found."));
            }
            
            return this.View(categories);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> Delete(string id)
        {
            await this._categoryRepository.Delete(id);
            
            this.TempData.AddSerialized<Alert>("Alerts", new Alert(AlertType.Success, "Successfully deleted category."));

            return this.RedirectToAction(nameof(this.All));
        }
    }
}