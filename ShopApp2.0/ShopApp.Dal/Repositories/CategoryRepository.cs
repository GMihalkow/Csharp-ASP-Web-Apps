using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.User.Contracts;
using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

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
                throw new ArgumentException("Category already exists!");
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
            var categoryEntity = this.GetCategoryAsDbEntity(((CategoryEditInputModel)model).Id);

            categoryEntity.Name = model.Name;
            categoryEntity.CoverUrl = model.CoverUrl;

            await this._dbContext.SaveChangesAsync();
        }

        public CategoryViewModel Get(string id)
        {
            var category = this.GetCategoryAsDbEntity(id);

            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                CoverUrl = category.CoverUrl
            };

            return categoryViewModel;
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

        private Category GetCategoryAsDbEntity(string id)
        {
            var categoryEntity =
                this._dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryEntity == null)
            {
                throw new ArgumentException("Invalid category id.");
            }

            return categoryEntity;
        }
    }
}