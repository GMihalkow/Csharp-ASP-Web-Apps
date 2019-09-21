using ShopApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Product.Contracts
{
    public interface IProductService
    {
        void AddProduct(ProductInputModel productModel);

        ProductViewModel RetrieveProduct(string id);

        int ProductsCountByCategory(string category);

        IEnumerable<ShopApp.Models.Product> GetAll();

        Task Delete(string productId);
    }
}