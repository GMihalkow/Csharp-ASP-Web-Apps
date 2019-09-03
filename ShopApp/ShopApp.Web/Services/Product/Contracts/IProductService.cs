using ShopApp.Web.Models;

namespace ShopApp.Web.Services.Product.Contracts
{
    public interface IProductService
    {
        void AddProduct(ProductInputModel productModel);
    }
}