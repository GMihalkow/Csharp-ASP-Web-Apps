using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.User.Contracts;
using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Dal.Repositories
{
    public class CategoryRepository : IRepository<CategoryViewModel, CategoryBaseInputModel>
    {
        private readonly ShopAppDbContext _dbContext;
        private readonly IUserService _userService;

        public CategoryRepository(ShopAppDbContext dbContext, IUserService userService)
        {
            this._dbContext = dbContext;
            this._userService = userService;
        }

        public async Task<CategoryBaseInputModel> Create(CategoryBaseInputModel model)
        {
            if (this._dbContext.Categories.Any(c => c.Name == model.Name))
            {
                throw new InvalidOperationException("Category already exists!");
            }
            
            var categoryEntity = new ShopApp.Models.Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                CoverUrl = model.CoverUrl,
                CreatedOn = DateTime.Now,
                CreatorId = model.CreatorId
            };

            this._dbContext.Categories.Add(categoryEntity);
            
            await this._dbContext.SaveChangesAsync();

            return model;
        }

        public async Task Delete(string id)
        {
            var categoryModel = this.Get(id);

            var categoryEntity = this._dbContext.Categories.FirstOrDefault(c => c.Id == categoryModel.Id);

            this._dbContext.Categories.Remove(categoryEntity);
            
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Edit(CategoryBaseInputModel model)
        {
            var categoryEntity = this._dbContext.Categories.FirstOrDefault(c => c.Id == ((CategoryEditInputModel)model).Id);

            if (categoryEntity != null)
            {
                categoryEntity.Name = model.Name;
                categoryEntity.CoverUrl = model.CoverUrl;

                await this._dbContext.SaveChangesAsync();
            }
        }

        public CategoryViewModel Get(string id)
        {
            var category = this.GetAll().FirstOrDefault(c => c.Id == id);

            if (category == null) { throw new InvalidOperationException("Category doesn't exist."); }

            return category;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var categories = this._dbContext.Categories
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