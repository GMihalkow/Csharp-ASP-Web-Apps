using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
using ShopApp.Web.Services.Account.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Web.Repositories
{
    public class CategoryRepository : IRepository<CategoryViewModel, CategoryInputModel>
    {
        private readonly ShopAppDbContext dbContext;
        private readonly IAccountService accountService;

        public CategoryRepository(ShopAppDbContext dbContext, IAccountService accountService)
        {
            this.dbContext = dbContext;
            this.accountService = accountService;
        }

        public async Task<CategoryInputModel> Create(CategoryInputModel model)
        {
            if (this.dbContext.Categories.Any(c => c.Name == model.Name))
            {
                throw new InvalidOperationException("Category already exists!");
            }

            ShopApp.Models.ShopUser user = await this.accountService.GetUserById(model.CreatorId);

            ShopApp.Models.Category categoryEntity = new ShopApp.Models.Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                CoverUrl = model.CoverUrl,
                CreatedOn = DateTime.UtcNow,
                CreatorId = user.Id
            };

            this.dbContext.Categories.Add(categoryEntity);
            await this.dbContext.SaveChangesAsync();

            return model;
        }

        public async Task Delete(string id)
        {
            CategoryViewModel categoryModel = this.Get(id);

            ShopApp.Models.Category categoryEntity = this.dbContext.Categories.FirstOrDefault(c => c.Id == categoryModel.Id);

            this.dbContext.Categories.Remove(categoryEntity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Edit(CategoryInputModel model)
        {
            ShopApp.Models.Category categoryEntity = this.dbContext.Categories.FirstOrDefault(c => c.Id == model.Id);

            if (categoryEntity != null)
            {
                categoryEntity.Name = model.Name;
                categoryEntity.CoverUrl = model.CoverUrl;

                await this.dbContext.SaveChangesAsync();
            }
        }

        public CategoryViewModel Get(string id)
        {
            CategoryViewModel category = this.GetAll()
              .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new InvalidOperationException("Category doesn't exist.");
            }

            return category;
        }

        public IEnumerable<CategoryViewModel> GetAll()
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
    }
}