using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Product
{
    public class ProductController : Controller
    {
        private readonly ICategoryService categoryService;

        public ProductController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult All()
        {
            List<CategoryViewModel> categories = this.categoryService.GetCategories().ToList();

            return this.View(categories);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create()
        {
            return this.RedirectToAction(this.Url.Action("All");
        }
    }
}