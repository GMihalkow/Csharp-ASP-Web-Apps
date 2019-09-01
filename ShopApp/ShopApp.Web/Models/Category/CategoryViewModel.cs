﻿using System.Collections.Generic;

namespace ShopApp.Web.Models
{
    public class CategoryInputModel
    { 
		public string Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<ProductViewModel> Products { get; set; }
	}
}