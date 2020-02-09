using System.ComponentModel.DataAnnotations;

namespace ShopApp.Dal
{
    public class CategoryEditInputModel : CategoryBaseInputModel
    {
        [Required]
        public string Id { get; set; }
    }
}