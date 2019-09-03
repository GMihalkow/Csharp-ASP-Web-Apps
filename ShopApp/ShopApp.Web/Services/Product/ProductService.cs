using Microsoft.AspNet.Identity.Owin;
using ShopApp.Data;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Web;

namespace ShopApp.Web.Services.Product
{
    public class ProductService : IProductService
    {
        private ShopAppDbContext dbContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<ShopAppDbContext>();
            }
        }
        // TODO [GM]: Make asynchronous?
        public void AddProduct(ProductInputModel productModel)
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
            this.dbContext.SaveChanges();
        }
    }
}