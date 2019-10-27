using ShopApp.Web.Models;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Product.Contracts
{
    public interface IProductService
    {
        int ProductsCountByCategory(string category);

        Task Create(ProductCreateModel model);

        Task Delete(string id);

        Task Edit(ProductEditModel model);

        ProductViewModel Get(string id);
    }
}