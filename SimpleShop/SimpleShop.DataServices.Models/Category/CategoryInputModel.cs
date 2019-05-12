using SimpleShop.DataServices.Models.Interfaces.Category;

namespace SimpleShop.DataServices.Models.Category
{
    public class CategoryInputModel : ICategoryInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}