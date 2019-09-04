using System;

namespace ShopApp.Web.Models
{
	public class ProductViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

        public string CoverUrl { get; set; }

        public DateTime? AddedOn { get; set; }
	}
}