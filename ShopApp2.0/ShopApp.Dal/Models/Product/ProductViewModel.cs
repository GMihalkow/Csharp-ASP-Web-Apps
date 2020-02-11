﻿using System;
 using System.ComponentModel.DataAnnotations;

 namespace ShopApp.Dal
{
	public class ProductViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

        public string Description { get; set; }

        [Range(minimum: 0.01, maximum: double.MaxValue)]
        public decimal Price { get; set; }

        public string CategoryId { get; set; }

        public string CoverUrl { get; set; }

        public DateTime? AddedOn { get; set; }

        public int StockCount { get; set; }
    }
}