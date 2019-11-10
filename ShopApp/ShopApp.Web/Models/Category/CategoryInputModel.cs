using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Web.Models
{
    public class CategoryInputModel
    {
        public string Id { get; set; }

		[Required]
        public string CreatorId { get; set; }

        [Required]
        [RegularExpression(pattern: GlobalConstants.UrlRegex, ErrorMessage = "You must provide a valid URL via HTTP or HTTPS.")]
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}