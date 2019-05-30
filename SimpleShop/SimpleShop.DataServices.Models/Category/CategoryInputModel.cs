using DataServices.Common;
using SimpleShop.DataServices.Models.Interfaces.Category;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.DataServices.Models.Category
{
    public class CategoryInputModel : ICategoryInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.MaxUsernameLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.MaxDescriptionLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }
    }
}