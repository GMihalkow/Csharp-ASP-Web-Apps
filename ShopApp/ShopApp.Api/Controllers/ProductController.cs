using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Dal.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopApp.Api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> productRepository;

        public ProductController(IProductService productService, ICategoryService categoryService, IRepository<ProductViewModel, ProductBaseInputModel> productRepository)
            : base()
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public JsonResult<ProductViewModel> Get(string id)
        {
            throw new Exception("TEST");
            ProductViewModel productModel = this.productRepository.Get(id);

            return this.Json(productModel);
        }

        [HttpGet]
        [Route(template: "api/Product/GetProductAdministrationModels")]
        public JsonResult<IEnumerable<ProductTableViewModel>> GetProductAdministrationModels()
        {
            return this.Json(this.productService.GetAdminViewProducts());
        }

        [HttpGet]
        [Route(template: "api/Product/Count")]
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