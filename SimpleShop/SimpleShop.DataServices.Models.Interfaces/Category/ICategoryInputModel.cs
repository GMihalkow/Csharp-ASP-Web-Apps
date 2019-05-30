namespace SimpleShop.DataServices.Models.Interfaces.Category
{
    public interface ICategoryInputModel
    {
        string Name { get; set; }

        string Description { get; set; }

        string CoverUrl { get; set; }
    }
}