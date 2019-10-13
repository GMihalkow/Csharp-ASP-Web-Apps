using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
	public class CategoryInputModel
	{
		public string Id { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]//TODO [GM]: Fix client validation
        public string CoverUrl { get; set; }
        [Required]
		public string Name { get; set; }
		public IEnumerable<ProductViewModel> Products { get; set; }
	}
}