using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Web.Constants;

namespace ShopApp.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IRepository<CategoryViewModel, CategoryBaseInputModel> _categoryRepository;

        public CategoryController(IRepository<CategoryViewModel, CategoryBaseInputModel> categoryRepository)
        {
            _categoryRepository = categoryRepository;
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

                // TODO [GM]: Return what?
                return this.Redirect("/");
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

                // TODO [GM]: Return what?
                return this.Redirect("/");
            }
            catch (ArgumentException e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);

                return this.View(inputModel);
            }
        }
    }
}