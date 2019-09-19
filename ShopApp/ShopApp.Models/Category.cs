using System;
using System.Collections.Generic;

namespace ShopApp.Models
{
	public class Category : BaseEntity<string>
	{
		public string Name { get; set; }
		public ICollection<Product> Products { get; set; }
		public DateTime CreatedOn { get; set; }
        public string CoverUrl { get; set; }
        public string CreatorId { get; set; }
		public ShopUser Creator { get; set; }
	}
}