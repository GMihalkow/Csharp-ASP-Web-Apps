﻿using System.Collections.Generic;

namespace ShopApp.Dal
{
    public class CategoryViewModel
    { 
		public string Id { get; set; }
		public string Name { get; set; }
        public string CoverUrl { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
	}
}