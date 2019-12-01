using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ICategoryService categoryService;
        private readonly ShopAppDbContext dbContext;
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> productRepository;

        public ProductService(ICategoryService categoryService, ShopAppDbContext dbContext, IRepository<ProductViewModel, ProductBaseInputModel> productRepository)
        {
            this.categoryService = categoryService;
            this.dbContext = dbContext;
            this.productRepository = productRepository;
        }

        public bool Exists(string id) => this.dbContext.Products.Any(product => product.Id == id);

        public async Task DecrementProductStockCount(string id, int quantity)
        {
            bool productExists = this.Exists(id);
            if (productExists)
            {
                ShopApp.Models.Product product = this.dbContext.Products.FirstOrDefault(p => p.Id == id);

                if (product.StockCount - quantity < 0)
                {
                    throw new InvalidOperationException($"Not enough quantity of {product.Name} product.");
                }

                product.StockCount = product.StockCount - quantity;
                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Invalid product id.");
            }
        }

        public int ProductsCountByCategory(string category)
        {
            string categoryId = this.categoryService.GetCategoryByName(category).Id;

            int productsCount = this.dbContext.Products.Where(c => c.CategoryId == categoryId).Count();

            return productsCount;
        }

        public IEnumerable<ProductTableViewModel> GetAdminViewProducts()
        {
            var products = this.dbContext.Products
                .Include(p => p.Category)
                .Select(p => new ProductTableViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AddedOn = p.AddedOn,
                    CategoryName = p.Category.Name,
                    StockCount = p.StockCount
                })
                .ToList();

            return products;
        }

        public async Task EditStockCount(string id, int stockCount)
        {
            if(stockCount < 0) { throw new ArgumentException("Invalid stock count."); }

            var product = this.dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                product.StockCount = stockCount;

                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}