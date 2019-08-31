using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
	public class CategoryViewModel
	{
		public string Id { get; set; }

		[Required]
		public string Name { get; set; }

		public IEnumerable<ProductViewModel> Products { get; set; }
	}
}