using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.User.Contracts;
using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Dal.Repositories
{
    public class CategoryRepository : IRepository<CategoryViewModel, CategoryInputModel>
    {
        private readonly ShopAppDbContext dbContext;
        private readonly IUserService userService;

        public CategoryRepository(ShopAppDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public async Task<CategoryInputModel> Create(CategoryInputModel model)
        {
            if (this.dbContext.Categories.Any(c => c.Name == model.Name))
            {
                throw new InvalidOperationException("Category already exists!");
            }

            ShopApp.Models.ShopUser user = this.userService.GetUserById(model.CreatorId);

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
            CategoryViewModel category = this.GetAll().FirstOrDefault(c => c.Id == id);

            if (category == null) { throw new InvalidOperationException("Category doesn't exist."); }

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