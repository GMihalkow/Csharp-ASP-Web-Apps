using System;
using System.Collections.Generic;

namespace ShopApp.Models
{
	public class Product : BaseEntity<string>
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
        public int StockCount { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
		public string CategoryId { get; set; }
		public Category Category { get; set; }
		public ICollection<Order> Orders { get; set; }
		public DateTime? AddedOn { get; set; }
	}
}