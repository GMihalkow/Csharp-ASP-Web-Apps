namespace ShopApp.Web.Services.Product.Contracts
{
    public interface IProductService
    {
        int ProductsCountByCategory(string category);
    }
}