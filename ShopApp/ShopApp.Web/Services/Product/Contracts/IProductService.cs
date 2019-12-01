using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Product.Contracts
{
    public interface IProductService
    {
        Task EditStockCount(string id, int stockCount);

        int ProductsCountByCategory(string category);

        bool Exists(string id);

        Task DecrementProductStockCount(string id, int quantity);

        IEnumerable<ProductTableViewModel> GetAdminViewProducts();
    }
}