using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ShopApp.Api.Controllers
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