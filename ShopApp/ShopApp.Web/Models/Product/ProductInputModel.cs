using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
    // TODO [GM]: Use a ProductBaseInputModel like at work?
    public class ProductInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [RegularExpression(pattern: GlobalConstants.UrlRegex, ErrorMessage = "You must provide a valid URL via HTTP or HTTPS.")]
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        public string CategoryId { get; set; }
    }
}