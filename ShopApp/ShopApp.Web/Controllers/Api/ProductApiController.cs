using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopApp.Web.Controllers.Api
{
    public class ProductApiController : BaseApiController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductApiController(IProductService productService, ICategoryService categoryService) : base()
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public JsonResult<ProductViewModel> Get(string id)
        {
            ProductViewModel productModel = this.productService.Get(id);

            return this.Json(productModel);
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