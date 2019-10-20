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
    public class ProductController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        // TODO [GM]: Make async?
        public ActionResult All(string category, int page = 0)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                category = this.categoryService.GetDefaultCategory();
                if (string.IsNullOrWhiteSpace(category))
                {
                    // TODO [GM]: throw exception and handle in global error handling filter
                    return this.HttpNotFound();
                }
            }

            List<CategoryViewModel> categories = this.categoryService.GetCategoriesWithProducts(category, page).ToList();

            return this.View(categories);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Create(ProductInputModel productModel)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException(this.ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage);
            }

            string categoryName = this.categoryService.GetCategory(productModel.CategoryId).Name;

            await this.productService.AddProduct(productModel);

            return this.Redirect("/Product/All?category=" + categoryName);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Edit(ProductInputModel productModel)
        {
            ;
            // TODO [GM]: Change category also?
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException(this.ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage);
            }

            var categoryName = this.categoryService.GetCategory(productModel.CategoryId).Name;

            await this.productService.EditProduct(productModel);

            return this.Redirect("/Product/All?category=" + categoryName);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Delete(string id)
        {
            await this.productService.Delete(id);
        }
    }
}