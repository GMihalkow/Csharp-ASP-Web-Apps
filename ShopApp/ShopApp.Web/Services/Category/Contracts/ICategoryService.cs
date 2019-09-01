using ShopApp.Web.Models;
using System.Collections.Generic;

namespace ShopApp.Web.Services.Category.Contracts
{
	public interface ICategoryService
	{
        CategoryViewModel GetCategory(string id);

		IEnumerable<CategoryViewModel> GetCategories();

        CategoryInputModel Create(CategoryInputModel model);
	}
}