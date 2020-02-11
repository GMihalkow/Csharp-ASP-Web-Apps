using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Dal.Services.Product.Contracts;
using ShopApp.Data;

namespace ShopApp.Dal.Services.Product
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(ShopAppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ProductTableViewModel> GetProductsAsTableModels()
        {
            var products = this._dbContext.Products
                .Include(product => product.Category)
                .Select(product =>
                    new ProductTableViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        AddedOn = product.AddedOn,
                        CategoryName = product.Category.Name,
                        StockCount = product.StockCount
                    })
                .ToList();

            return products;
        }
    }
}