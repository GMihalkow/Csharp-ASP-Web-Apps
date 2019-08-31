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
			List<CategoryViewModel> categories = new List<CategoryViewModel>
				{
					new CategoryViewModel { Id = Guid.NewGuid().ToString(), Name = "Mens" },
					new CategoryViewModel { Id = Guid.NewGuid().ToString(), Name = "Womens" },
					new CategoryViewModel { Id = Guid.NewGuid().ToString(), Name = "Sportswear" }
				};

			return this.View(categories);
		}
	}
}