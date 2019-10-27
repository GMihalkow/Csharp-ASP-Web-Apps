using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ICategoryService categoryService;
        private readonly ShopAppDbContext dbContext;
        
        public ProductService(ICategoryService categoryService, ShopAppDbContext dbContext)
        {
            this.categoryService = categoryService;
            this.dbContext = dbContext;
        }

        public int ProductsCountByCategory(string category)
        {
            string categoryId = this.categoryService.GetCategoryByName(category).Id;

            int productsCount = this.dbContext.Products.Where(c => c.CategoryId == categoryId).Count();

            return productsCount;
        }

        public async Task Create(ProductCreateModel model)
        {
            ShopApp.Models.Product productEntity = new ShopApp.Models.Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                AddedOn = DateTime.UtcNow,
                Description = model.Description,
                CoverUrl = model.CoverUrl,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            this.dbContext.Products.Add(productEntity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            ShopApp.Models.Product product = this.dbContext.Products.FirstOrDefault(p => p.Id == id);

            // we delete the product only if it already exists
            if (product != null)
            {
                this.dbContext.Products.Remove(product);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task Edit(ProductEditModel model)
        {
            ShopApp.Models.Product productEntity =
              this.dbContext
              .Products
              .FirstOrDefault(product => product.Id == model.Id);

            if (productEntity == null)
            {
                throw new InvalidOperationException("Invalid product id!");
            }

            productEntity.Name = model.Name;
            productEntity.Price = model.Price;
            productEntity.Description = model.Description;
            productEntity.CoverUrl = model.CoverUrl;
            productEntity.CategoryId = model.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        public ProductViewModel Get(string id)
        {
            ProductViewModel productModel = this.dbContext.Products
                .Where(product => product.Id == id)
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name,
                    CoverUrl = product.CoverUrl,
                    AddedOn = product.AddedOn,
                    Description = product.Description
                })
                .FirstOrDefault();

            if (productModel == null)
            {
                throw new InvalidOperationException("Product doesn't exist.");
            }

            return productModel;
        }
    }
}