using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApp.Dal.Services.Category
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly string[] _allowedSortColumns = new string[] {"AddedOn", "Name", "Price"};

        private readonly IRepository<CategoryViewModel, CategoryBaseInputModel> _categoryRepository;

        public CategoryService(ShopAppDbContext dbContext,
            IRepository<CategoryViewModel, CategoryBaseInputModel> categoryRepository) : base(dbContext)
        {
            this._categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryViewModel> GetCategoriesWithProductsForSelectedCategory(string categoryName,
            int page, string keywords, string sortBy = "", bool sortDesc = false)
        {
            if (page < 0)
            {
                page = 0;
            }

            var categories = this._dbContext
                .Categories
                .Include(c => c.Products)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CoverUrl = c.CoverUrl
                })
                .ToList();

            var selectedCategory = categories.FirstOrDefault(c => c.Name == categoryName);

            if (selectedCategory == null)
            {
                throw new InvalidOperationException("Category not found.");
            }

            selectedCategory.Products = this._dbContext.Products
                .Where(p => p.CategoryId == selectedCategory.Id)
                .Where(p => p.Name.Contains(keywords) || p.Description.Contains(keywords))
                .OrderBy(p => p.AddedOn)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    CoverUrl = p.CoverUrl,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    AddedOn = p.AddedOn,
                    StockCount = p.StockCount
                })
                .Skip(page * DalConstants.PageSize)
                .Take(DalConstants.PageSize)
                .ToList();

            if (!string.IsNullOrEmpty(sortBy) && this._allowedSortColumns.Contains(sortBy))
            {
                if (sortDesc)
                {
                    selectedCategory.Products = selectedCategory.Products.OrderByDescending(sortBy);
                }
                else
                {
                    selectedCategory.Products = selectedCategory.Products.OrderBy(sortBy);
                }
            }
            else
            {
                // default sorting
                selectedCategory.Products = selectedCategory.Products.OrderBy(pr => pr.Price);
            }

            return categories;
        }

        public CategoryViewModel GetCategoryByName(string name)
        {
            var categoryModel = this._dbContext.Categories
                .Where(c => c.Name == name)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CoverUrl = c.CoverUrl,
                    Products = c.Products.Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        CoverUrl = p.CoverUrl,
                        AddedOn = p.AddedOn,
                        CategoryId = p.CategoryId,
                        Description = p.Description,
                        Name = p.Name,
                        Price = p.Price
                    })
                })?
                .FirstOrDefault();

            if (categoryModel == null)
            {
                throw new InvalidOperationException("Invalid category name.");
            }

            return categoryModel;
        }

        public string GetDefaultCategory()
        {
            ShopApp.Models.Category defaultCategory = this._dbContext.Categories.FirstOrDefault();

            if (defaultCategory == null)
            {
                return string.Empty;
            }

            return defaultCategory.Name;
        }

        public async Task<IEnumerable<CategoryNavBarViewModel>> GetCategoriesForDropdown()
        {
            return await this._dbContext.Categories
                .Select((category) => new CategoryNavBarViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoryTableViewModel>> GetCategoriesForTable()
        {
            var categories = await this._dbContext.Categories.Include(category => category.Creator)
                .Select(category => new CategoryTableViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreatedOn = category.CreatedOn,
                    CreatorName = category.Creator.UserName
                })
                .ToListAsync();

            return categories;
        }
    }
}