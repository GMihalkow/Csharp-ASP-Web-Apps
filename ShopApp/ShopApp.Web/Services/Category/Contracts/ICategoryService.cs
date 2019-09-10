using ShopApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Category.Contracts
{
	public interface ICategoryService
	{
        CategoryViewModel GetCategory(string id);

		IEnumerable<CategoryViewModel> GetCategoriesWithProducts();

        IEnumerable<CategoryViewModel> GetCategories();

        CategoryInputModel Create(CategoryInputModel model);

        Task Delete(string id);
	}
}