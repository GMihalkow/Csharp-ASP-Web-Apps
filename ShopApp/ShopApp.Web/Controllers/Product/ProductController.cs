using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ActionResult All(string category = "Womens", int page = 0)
        {
            List<CategoryViewModel> categories = this.categoryService.GetCategoriesWithProducts(category, page).ToList();

            return this.View(categories);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public ActionResult Create(ProductInputModel productModel)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException(this.ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage);
            }

            string categoryName = this.categoryService.GetCategory(productModel.CategoryId).Name;

            this.productService.AddProduct(productModel);

            return this.Redirect("/Product/All?category=" + categoryName);
        }

        public ActionResult Count(string category = "Womens")
        {
            int productsCount = this.productService.ProductsCountByCategory(category);

            return this.Json(productsCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Names()
        {
            string[] productsNames = this.productService.GetAll().Select(p => p.Name).ToArray();

            return this.Json(productsNames, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Delete(string id)
        {
            await this.productService.Delete(id);
        }
    }
}