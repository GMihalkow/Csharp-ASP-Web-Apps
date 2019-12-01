using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers
{
    // TODO [GM]: Create a supply products functionality? (Add product stock count control?)
    public class ProductController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IRepository<CategoryViewModel, CategoryInputModel> categoryRepository;
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> productRepository;

        public ProductController(ICategoryService categoryService, IProductService productService, IRepository<CategoryViewModel, CategoryInputModel> categoryRepository, IRepository<ProductViewModel, ProductBaseInputModel> productRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.categoryService = categoryService;
            this.productService = productService;
        }

        // TODO [GM]: Make async?
        public ActionResult All(string category, string keywords = "", int page = 0, string sortBy = "", bool sortDesc = false)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                category = this.categoryService.GetDefaultCategory();
                if (string.IsNullOrWhiteSpace(category))
                {
                    return this.View(new List<CategoryViewModel>());
                }
            }

            IEnumerable<CategoryViewModel> categories = this.categoryService.GetCategoriesWithProductsForSelectedCategory(category, page, keywords, sortBy: sortBy, sortDesc: sortDesc);

            this.ViewBag.Keywords = keywords;
            this.ViewBag.SortBy = sortBy;
            this.ViewBag.SortDesc = sortDesc;

            return this.View(categories);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Create(ProductCreateModel productModel)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException(this.ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage);
            }

            string categoryName = this.categoryRepository.Get(productModel.CategoryId).Name;

            await this.productRepository.Create(productModel);

            return this.Redirect("/Product/All?category=" + categoryName);
        }

        [HttpPut]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task EditStockCount(string id, int stockCount)
        {
            await this.productService.EditStockCount(id, stockCount);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<ActionResult> Edit(ProductEditModel productModel)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException(this.ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage);
            }

            var categoryName = this.categoryRepository.Get(productModel.CategoryId).Name;

            await this.productRepository.Edit(productModel);

            return this.Redirect("/Product/All?category=" + categoryName);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Delete(string id)
        {
            await this.productRepository.Delete(id);
        }
    }
}