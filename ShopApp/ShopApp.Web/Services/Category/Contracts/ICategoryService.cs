using ShopApp.Web.Models;
using System.Collections.Generic;

namespace ShopApp.Web.Services.Category.Contracts
{
	public interface ICategoryService
	{
		IEnumerable<CategoryViewModel> GetCategories();
	}
}