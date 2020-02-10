using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Web.Constants;

namespace ShopApp.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IRepository<CategoryViewModel, CategoryBaseInputModel> _categoryRepository;
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> _productRepository;

        public ProductController(IRepository<CategoryViewModel, CategoryBaseInputModel> categoryRepository,
            IRepository<ProductViewModel, ProductBaseInputModel> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
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

                // TODO [GM]: Return what?
                return this.Redirect("/");
            }
            catch (Exception e)
            {
                // TODO [GM]: Add alerts?
                return this.View(inputModel);
            }
        }
    }
}