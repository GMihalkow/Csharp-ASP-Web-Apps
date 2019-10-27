﻿using ShopApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Category.Contracts
{
	public interface ICategoryService
	{
        CategoryViewModel GetCategoryByName(string name);

        CategoryViewModel GetCategory(string id);

		IEnumerable<CategoryViewModel> GetCategoriesWithProducts(string categoryName, int page);

        IEnumerable<CategoryViewModel> GetCategories();

        Task<CategoryInputModel> Create(CategoryInputModel model);

        Task Delete(string id);

        Task Edit(CategoryInputModel model);

        string GetDefaultCategory();
	}
}