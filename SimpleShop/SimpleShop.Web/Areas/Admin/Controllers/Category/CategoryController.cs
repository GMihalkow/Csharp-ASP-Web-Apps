using DataServices.Common;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.DataServices.Interfaces.Category;
using SimpleShop.DataServices.Interfaces.Db;
using SimpleShop.DataServices.Models.Category;
using SimpleShop.Web.Controllers;
using SimpleShop.Web.Attributes.Authorize;
using SimpleShop.Web.Filters.Exception;

namespace SimpleShop.Web.Areas.Admin.Controllers.Category
{
    [Area("Admin")]
    [AuthorizeRoles(new string[] { Role.Administrator, Role.Owner })]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(IDbService dbService, ICategoryService categoryService)
            : base(dbService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [ErrorViewFilter]
        [HttpPost]
        public IActionResult Create(CategoryInputModel model)
        {
            this.categoryService.Create(model, this.User);

            this.TempData["Message"] = $"Successfully created the {model.Name} category.";
            
            return this.Redirect("/Admin/Category/Create");
        }
    }
}