 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using Microsoft.AspNetCore.Mvc.Rendering;

 namespace ShopApp.Dal
{
    public class ProductBaseInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Required]
        [RegularExpression(pattern: DalConstants.UrlRegex, ErrorMessage = "You must provide a valid URL via HTTP or HTTPS.")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Cover Url")]
        public string CoverUrl { get; set; }
        
        [Required]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}