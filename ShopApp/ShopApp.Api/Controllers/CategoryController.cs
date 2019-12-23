using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using System.Web.Http.Results;

namespace ShopApp.Api.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly IRepository<CategoryViewModel, CategoryInputModel> categoryRepository;

        public CategoryController(ICategoryService categoryService, IRepository<CategoryViewModel, CategoryInputModel> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public JsonResult<CategoryViewModel> Get(string id)
        {
            CategoryViewModel categoryModel = this.categoryRepository.Get(id);

            return this.Json(categoryModel);
        }
    }
}