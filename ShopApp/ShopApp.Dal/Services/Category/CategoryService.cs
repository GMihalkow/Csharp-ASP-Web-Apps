using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Dal.Services.Category.Contracts;
using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopApp.Dal.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly string[] allowedSortColumns = new string[] { "AddedOn", "Name", "Price" };

        private readonly ShopAppDbContext dbContext;
        private readonly IRepository<CategoryViewModel, CategoryInputModel> categoryRepository;

        // TODO [GM]: dbContext not needed here? use categoryRepository.GetAll() ?
        public CategoryService(ShopAppDbContext dbContext, IRepository<CategoryViewModel, CategoryInputModel> categoryRepository)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryViewModel> GetCategoriesWithProductsForSelectedCategory(string categoryName, int page, string keywords, string sortBy = "", bool sortDesc = false)
        {
            // filtering invalid negative inputs
            if (page < 0) { page = 0; }

            List<CategoryViewModel> categories = this.dbContext
                    .Categories
                    .Include(c => c.Products)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CoverUrl = c.CoverUrl
                    })
                    .ToList();

            CategoryViewModel selectedCategory = categories.FirstOrDefault(c => c.Name == categoryName);

            if (selectedCategory == null) { throw new InvalidOperationException("Category not found."); }

            selectedCategory.Products = this.dbContext.Products
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

            if (!string.IsNullOrEmpty(sortBy) && this.allowedSortColumns.Contains(sortBy))
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
            CategoryViewModel categoryModel = this.dbContext.Categories
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

            if (categoryModel == null) { throw new InvalidOperationException("Invalid category name."); }

            return categoryModel;
        }

        public string GetDefaultCategory()
        {
            ShopApp.Models.Category defaultCategory = this.dbContext.Categories.FirstOrDefault();

            if (defaultCategory == null) { return string.Empty; }

            return defaultCategory.Name;
        }
    }
}