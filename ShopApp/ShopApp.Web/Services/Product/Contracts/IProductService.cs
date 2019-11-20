using System.Threading.Tasks;

namespace ShopApp.Web.Services.Product.Contracts
{
    public interface IProductService
    {
        int ProductsCountByCategory(string category);

        bool Exists(string id);

        Task DecrementProductStockCount(string id, int quantity);
    }
}