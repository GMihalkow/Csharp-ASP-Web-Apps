using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Product
{
    public class ProductController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public ActionResult GetProduct(string id)
        {
            ProductViewModel productModel = this.productService.RetrieveProduct(id);

            return this.Json(productModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult All()
        {
            List<CategoryViewModel> categories = this.categoryService.GetCategoriesWithProducts().ToList();

            return this.View(categories);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public ActionResult Create(ProductInputModel productModel)
        {
            if (!this.ModelState.IsValid)
            {
                // TODO [GM]: Handle error properly
            }
            
            this.productService.AddProduct(productModel);

            return this.RedirectToAction("All");
        }
    }
}