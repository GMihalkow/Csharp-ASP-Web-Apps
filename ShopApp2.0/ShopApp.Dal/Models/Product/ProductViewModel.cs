﻿using System;

namespace ShopApp.Dal
{
	public class ProductViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CoverUrl { get; set; }

        public DateTime? AddedOn { get; set; }

        public int StockCount { get; set; }
    }
}