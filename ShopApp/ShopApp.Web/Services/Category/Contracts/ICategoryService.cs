using ShopApp.Web.Models;
using System.Collections.Generic;

namespace ShopApp.Web.Services.Category.Contracts
{
    public interface ICategoryService
	{
        CategoryViewModel GetCategoryByName(string name);

		IEnumerable<CategoryViewModel> GetCategoriesWithProducts(string categoryName, int page, string keywords);

        string GetDefaultCategory();
	}
}