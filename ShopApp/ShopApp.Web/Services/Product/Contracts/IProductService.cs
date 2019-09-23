using ShopApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Product.Contracts
{
    public interface IProductService
    {
        Task AddProduct(ProductInputModel productModel);

        Task EditProduct(ProductInputModel productModel);

        ProductViewModel RetrieveProduct(string id);

        int ProductsCountByCategory(string category);

        IEnumerable<ShopApp.Models.Product> GetAll();

        Task Delete(string productId);
    }
}