using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Dal.Services.Category.Contracts
{
    public interface ICategoryService
    {
        CategoryViewModel GetCategoryByName(string name);

        IEnumerable<CategoryViewModel> GetCategoriesWithProductsForSelectedCategory(string categoryName, int page, string keywords, string sortBy = "", bool sortDesc = false);

        string GetDefaultCategory();

        Task<IEnumerable<CategoryNavBarViewModel>> GetCategoriesForDropdown();
    }
}