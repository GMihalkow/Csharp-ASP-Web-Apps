using ShopApp.Data;
using ShopApp.Web.Services.Category.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System.Linq;

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
    }
}