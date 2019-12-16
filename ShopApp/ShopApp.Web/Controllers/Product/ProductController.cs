using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Dal.Services.Product.Contracts;
using ShopApp.Web.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers
{
    [Authorize(Roles = RolesConstants.Administrator)]
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

        [AllowAnonymous]
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
        public async Task EditStockCount(string id, int stockCount)
        {
            await this.productService.EditStockCount(id, stockCount);
        }

        [HttpPost]
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

        public async Task Delete(string id)
        {
            await this.productRepository.Delete(id);
        }
    }
}