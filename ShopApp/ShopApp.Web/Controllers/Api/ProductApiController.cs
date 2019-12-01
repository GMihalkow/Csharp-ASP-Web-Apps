using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopApp.Web.Controllers.Api
{
    public class ProductApiController : BaseApiController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> productRepository;

        public ProductApiController(IProductService productService, ICategoryService categoryService, IRepository<ProductViewModel, ProductBaseInputModel> productRepository)
            : base()
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.productRepository = productRepository;
        }

        public JsonResult<ProductViewModel> Get(string id)
        {
            ProductViewModel productModel = this.productRepository.Get(id);

            return this.Json(productModel);
        }

        [HttpGet]
        [Route(template: "api/ProductApi/GetProductAdministrationModels")]
        [Authorize(Roles = RolesConstants.Administrator)]
        public JsonResult<IEnumerable<ProductTableViewModel>> GetProductAdministrationModels()
        {
            return this.Json<IEnumerable<ProductTableViewModel>>(this.productService.GetAdminViewProducts());
        }

        [HttpGet]
        [Route(template: "api/ProductApi/Count")]
        public JsonResult<int> Count(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                category = this.categoryService.GetDefaultCategory();
            }

            int productsCount = this.productService.ProductsCountByCategory(category);

            return this.Json(productsCount);
        }
    }
}