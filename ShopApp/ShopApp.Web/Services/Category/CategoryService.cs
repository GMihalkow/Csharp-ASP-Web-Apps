using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Web.Services.Category.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Web.Services.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly IAccountService accountService;
        private readonly ShopAppDbContext dbContext;
        private readonly IRepository<CategoryViewModel, CategoryInputModel> categoryRepository;

        public CategoryService(IAccountService accountService, ShopAppDbContext dbContext, IRepository<CategoryViewModel, CategoryInputModel> categoryRepository)
        {
            this.accountService = accountService;
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryViewModel> GetCategoriesWithProducts(string categoryName, int page, string keywords)
        {
            // filtering invalid negative inputs
            if (page < 0)
            {
                page = 0;
            }

            List<CategoryViewModel> categories =
                this.dbContext
                .Categories
                .Include("Products")
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CoverUrl = c.CoverUrl
                })
                .ToList();
            // TODO [GM]: Make a method in the ProductService for this?
            CategoryViewModel selectedCategory = categories.FirstOrDefault(c => c.Name == categoryName);

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
                    AddedOn = p.AddedOn
                })
                .Skip(page * GlobalConstants.PageSize)
                .Take(GlobalConstants.PageSize)
                .ToList();

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
                })?.FirstOrDefault();

            if (categoryModel == null)
            {
                throw new InvalidOperationException("Invalid category name.");
            }

            return categoryModel;
        }

        public string GetDefaultCategory()
        {
            // TODO [GM]: make async?
            // TODO [GM]: don't pull all categories?
            var defaultCategory = this.categoryRepository.GetAll().FirstOrDefault();

            if (defaultCategory == null)
            {
                return string.Empty;
            }

            return defaultCategory.Name;
        }
    }
}