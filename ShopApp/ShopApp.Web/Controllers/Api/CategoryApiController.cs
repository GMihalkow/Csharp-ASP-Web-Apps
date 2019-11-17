using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
using ShopApp.Web.Services.Category.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Api
{
    public class CategoryApiController : BaseApiController
    {
        private readonly IRepository<CategoryViewModel, CategoryInputModel> categoryRepository;

        public CategoryApiController(ICategoryService categoryService, IRepository<CategoryViewModel, CategoryInputModel> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [Authorize]
        public JsonResult<CategoryViewModel> Get(string id)
        {
            CategoryViewModel categoryModel = this.categoryRepository.Get(id);

            return this.Json(categoryModel);
        }
    }
}