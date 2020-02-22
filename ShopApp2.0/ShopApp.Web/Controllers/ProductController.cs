using System;
using System.Collections.Generic;
using ShopApp.Web.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Product.Contracts;
using ShopApp.Web.Constants;
using ShopApp.Web.Models;

namespace ShopApp.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IRepository<CategoryViewModel, CategoryBaseInputModel> _categoryRepository;
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> _productRepository;
        private readonly IProductService _productService;

        public ProductController(IRepository<CategoryViewModel, CategoryBaseInputModel> categoryRepository,
            IRepository<ProductViewModel, ProductBaseInputModel> productRepository,
            IProductService productService)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        // TODO [GM]: Implement
        public IActionResult Search(string categoryName)
        {
            return this.View();
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public IActionResult Create()
        {
            var model = new ProductBaseInputModel
            {
                Categories = this._categoryRepository.GetAll()
                    .ToSelectList(category => category.Name, category => category.Id)
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> Create(ProductBaseInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Categories = this._categoryRepository.GetAll()
                    .ToSelectList(category => category.Name, category => category.Id);

                return this.View(inputModel);
            }

            try
            {
                await this._productRepository.Create(inputModel);

                this.TempData.AddSerialized<Alert>("Alerts",
                    new Alert(AlertType.Success, "Successfully created a new product."));

                return this.RedirectToAction(nameof(this.All));
            }
            catch (ArgumentException e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);

                return this.View(inputModel);
            }
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public IActionResult All()
        {
            var products = this._productService.GetProductsAsTableModels();

            if (products.IsNullOrEmpty())
            {
                this.TempData.AddSerialized<Alert>("Alerts",
                    new Alert(AlertType.Info, "No products found."));
            }

            return this.View(products);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public IActionResult Edit(string id)
        {
            try
            {
                var productViewModel = this._productRepository.Get(id);

                var productEditModel = new ProductEditInputModel()
                {
                    Id = productViewModel.Id,
                    Description = productViewModel.Description,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price,
                    CategoryId = productViewModel.CategoryId,
                    CoverUrl = productViewModel.CoverUrl,
                    Categories = this._categoryRepository.GetAll()
                        .ToSelectList(category => category.Name, category => category.Id)
                };

                return this.View(productEditModel);
            }
            catch (ArgumentException)
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> Edit(ProductEditInputModel inputModel)
        {
            try
            {
                await this._productRepository.Edit(inputModel);

                this.TempData.AddSerialized<Alert>("Alerts",
                    new Alert(AlertType.Success, "Successfully edited product."));

                return this.RedirectToAction(nameof(this.All));
            }
            catch (ArgumentException e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);

                return this.View(inputModel);
            }
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await this._productRepository.Delete(id);

                this.TempData.AddSerialized<Alert>("Alerts",
                    new Alert(AlertType.Success, "Product successfully deleted."));
            }
            catch (ArgumentException e)
            {
                this.TempData.AddSerialized<Alert>("Alerts", new Alert(AlertType.Error, e.Message));
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}