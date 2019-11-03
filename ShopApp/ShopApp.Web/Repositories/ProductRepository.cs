using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Web.Repositories
{
    public class ProductRepository : IRepository<ProductViewModel, ProductBaseInputModel>
    {
        private readonly ShopAppDbContext dbContext;

        public ProductRepository(ShopAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductBaseInputModel> Create(ProductBaseInputModel model)
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

            return model;
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

        public async Task Edit(ProductBaseInputModel model)
        {
            ShopApp.Models.Product productEntity =
            this.dbContext
            .Products
            .FirstOrDefault(product => product.Id == ((ProductEditModel)model).Id);

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

        public IEnumerable<ProductViewModel> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}