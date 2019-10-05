using Microsoft.AspNet.Identity.Owin;
using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShopApp.Web.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ICategoryService categoryService;

        private ShopAppDbContext dbContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<ShopAppDbContext>();
            }
        }

        public ProductService(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task AddProduct(ProductInputModel productModel)
        {
            ShopApp.Models.Product productEntity = new ShopApp.Models.Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = productModel.Name,
                AddedOn = DateTime.UtcNow,
                Description = productModel.Description,
                CoverUrl = productModel.CoverUrl,
                Price = productModel.Price,
                CategoryId = productModel.CategoryId
            };

            this.dbContext.Products.Add(productEntity);
            await this.dbContext.SaveChangesAsync();
        }

        public int ProductsCountByCategory(string category)
        {
            string categoryId = this.categoryService.GetCategoryByName(category).Id;

            int productsCount = this.dbContext.Products.Where(c => c.CategoryId == categoryId).Count();

            return productsCount;
        }

        public ProductViewModel RetrieveProduct(string id)
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

        public IEnumerable<ShopApp.Models.Product> GetAll()
        {
            IEnumerable<ShopApp.Models.Product> products = this.dbContext.Products.Include("Category").ToList();

            return products;
        }

        public async Task Delete(string productId)
        {
            ShopApp.Models.Product product = this.dbContext.Products.FirstOrDefault(p => p.Id == productId);

            // we delete the product only if it already exists
            if (product != null)
            {
                this.dbContext.Products.Remove(product);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task EditProduct(ProductInputModel productModel)
        {
            ShopApp.Models.Product productEntity =
              this.dbContext
              .Products
              .FirstOrDefault(product => product.Id == productModel.Id);

            if (productEntity != null)
            {
                productEntity.Name = productModel.Name;
                productEntity.Price = productModel.Price;
                productEntity.Description = productModel.Description;
                productEntity.CoverUrl = productModel.CoverUrl;
                productEntity.CategoryId = productModel.CategoryId;

                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}