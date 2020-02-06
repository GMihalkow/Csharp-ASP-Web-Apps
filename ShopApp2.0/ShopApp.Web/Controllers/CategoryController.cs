using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Web.Constants;

namespace ShopApp.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IRepository<CategoryViewModel, CategoryBaseInputModel> _categoryRepository;

        public CategoryController(IRepository<CategoryViewModel, CategoryBaseInputModel> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        [Authorize(Roles = RolesConstants.Administrator)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task<IActionResult> Create(CategoryBaseInputModel categoryInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(categoryInputModel);
            }
            
            try
            {
                categoryInputModel.CreatorId = this.LoggedUserId; 
                
                await this._categoryRepository.Create(categoryInputModel);
                
                return this.Redirect("/");
            }
            catch (InvalidOperationException e)
            {
                // TODO [GM]: Add alerts?
                return this.View(categoryInputModel);
            }
        }
    }
}