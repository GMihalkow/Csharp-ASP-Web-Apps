using Microsoft.AspNet.Identity.Owin;
using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Linq;
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

        // TODO [GM]: Make asynchronous?
        public void AddProduct(ProductInputModel productModel)
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
            }
            else
            {
                productEntity = new ShopApp.Models.Product
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
            }

            this.dbContext.SaveChanges();
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