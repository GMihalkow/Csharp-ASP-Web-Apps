using ShopApp.Web.Models;
using System.Collections.Generic;

namespace ShopApp.Web.Services.Category.Contracts
{
    public interface ICategoryService
    {
        CategoryViewModel GetCategoryByName(string name);

        IEnumerable<CategoryViewModel> GetCategoriesWithProductsForSelectedCategory(string categoryName, int page, string keywords, string sortBy = "", bool sortDesc = false);

        string GetDefaultCategory();
    }
}