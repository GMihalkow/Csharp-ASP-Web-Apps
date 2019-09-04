using System.Collections.Generic;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Data;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using ShopApp.Web.Models;
using System.Threading.Tasks;
using System;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Models;

namespace ShopApp.Web.Services.Category
{
	public class CategoryService : ICategoryService
	{
        public readonly IAccountService accountService;

        public CategoryService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

		private ShopAppDbContext dbContext
		{
			get
			{
				return HttpContext.Current.GetOwinContext().Get<ShopAppDbContext>();
			}
		}

        public CategoryInputModel Create(CategoryInputModel model)
        {
            if(this.dbContext.Categories.Any(c => c.Name == model.Name))
            {
                throw new InvalidOperationException("Category already exists!");
            }

            ShopApp.Models.ShopUser user = this.accountService.GetUser(HttpContext.Current.User.Identity.Name);

            ShopApp.Models.Category categoryEntity = new ShopApp.Models.Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
                CreatorId = user.Id
            };

            this.dbContext.Categories.Add(categoryEntity);
            this.dbContext.SaveChanges();

            return model;
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
                        CoverUrl = p.CoverUrl,
						Price = p.Price
					})
				}).ToList();

			return categories;
		}

        public CategoryViewModel GetCategory(string id)
        {
            CategoryViewModel category = this.GetCategories()
                .FirstOrDefault(c => c.Id == id);

            return category;
        }
    }
}