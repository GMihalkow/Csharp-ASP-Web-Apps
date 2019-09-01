﻿using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
    public class ProductInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }
    }
}