using System.Collections.Generic;

namespace ShopApp.Dal.Services.Product.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductTableViewModel> GetProductsAsTableModels();
    }
}