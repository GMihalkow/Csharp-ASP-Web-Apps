using System.Collections.Generic;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Data;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using ShopApp.Web.Models;

namespace ShopApp.Web.Services.Category
{
	public class CategoryService : ICategoryService
	{
		private ShopAppDbContext dbContext
		{
			get
			{
				return HttpContext.Current.GetOwinContext().Get<ShopAppDbContext>();
			}
		}

		public IEnumerable<CategoryViewModel> GetCategories()
		{
			List<CategoryViewModel> categories =
				this.dbContext
				.Categories
				.Include("Products")
				.Select(c => new CategoryViewModel
				{
					Id = c.Id,
					Name = c.Name,
					Products = c.Products.Select(p => new ProductViewModel
					{
						Id = p.Id,
						Name = p.Name,
						AddedOn = p.AddedOn,
						Price = p.Price
					})
				}).ToList();

			return categories;
		}
	}
}