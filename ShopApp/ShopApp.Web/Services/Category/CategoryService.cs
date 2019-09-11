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

        private bool EditCategory(CategoryInputModel model)
        {
            ShopApp.Models.Category categoryEntity = this.dbContext.Categories.FirstOrDefault(c => c.Id == model.Id);
            
            if(categoryEntity != null)
            {
                categoryEntity.Name = model.Name;
                categoryEntity.CoverUrl = model.CoverUrl;

                this.dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public CategoryInputModel CreateOrEdit(CategoryInputModel model)
        {
            if (this.dbContext.Categories.Any(c => c.Name == model.Name))
            {
                throw new InvalidOperationException("Category already exists!");
            }

            // TODO [GM]: Two separate methods ?
            // editing the entity if it already exists
            if (this.EditCategory(model))
            {
                return model;
            }

            ShopApp.Models.ShopUser user = this.accountService.GetUser(HttpContext.Current.User.Identity.Name);

            ShopApp.Models.Category categoryEntity = new ShopApp.Models.Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                CoverUrl = model.CoverUrl,
                CreatedOn = DateTime.UtcNow,
                CreatorId = user.Id
            };

            this.dbContext.Categories.Add(categoryEntity);
            this.dbContext.SaveChanges();

            return model;
        }

        public async Task Delete(string id)
        {
            CategoryViewModel categoryModel = this.GetCategory(id);

            ShopApp.Models.Category categoryEntity = this.dbContext.Categories.FirstOrDefault(c => c.Id == categoryModel.Id);

            this.dbContext.Categories.Remove(categoryEntity);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            List<CategoryViewModel> categories = this.dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    CoverUrl = c.CoverUrl,
                    Name = c.Name
                })
                .ToList();

            return categories;
        }

        public IEnumerable<CategoryViewModel> GetCategoriesWithProducts()
        {
            List<CategoryViewModel> categories =
                this.dbContext
                .Categories
                .Include("Products")
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CoverUrl = c.CoverUrl,
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
            CategoryViewModel category = this.GetCategoriesWithProducts()
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new InvalidOperationException("Category doesn't exist.");
            }

            return category;
        }
    }
}