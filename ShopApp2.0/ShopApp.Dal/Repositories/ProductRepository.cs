using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Dal.Repositories.Contracts;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp.Dal.Repositories
{
    public class ProductRepository : IRepository<ProductViewModel, ProductBaseInputModel>
    {
        private readonly ShopAppDbContext _dbContext;
        private readonly IRepository<CategoryViewModel, CategoryBaseInputModel> _categoryRepository;

        public ProductRepository(ShopAppDbContext dbContext, IRepository<CategoryViewModel, CategoryBaseInputModel> categoryRepository)
        {
            _dbContext = dbContext;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return this._dbContext.Products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                AddedOn = product.AddedOn,
                CategoryId = product.CategoryId,
                CoverUrl = product.CoverUrl,
                StockCount = product.StockCount
            }).ToList();
        }

        public ProductViewModel Get(string id)
        {
            var productEntity = this.GetProductAsDbEntity(id);

            var productViewModel = new ProductViewModel
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Price = productEntity.Price,
                AddedOn = productEntity.AddedOn,
                CategoryId = productEntity.CategoryId,
                CoverUrl = productEntity.CoverUrl,
                StockCount = productEntity.StockCount
            };

            return productViewModel;
        }

        public async Task<ProductBaseInputModel> Create(ProductBaseInputModel model)
        {
            var category = this._categoryRepository.Get(model.CategoryId);

            if (category == null)
            {
                throw new ArgumentException("Inavalid Category Id.");
            }
            
            var productEntity = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                AddedOn = DateTime.Now,
                CategoryId = model.CategoryId,
                CoverUrl = model.CoverUrl,
                StockCount = 0
            };

            await this._dbContext.Products.AddAsync(productEntity);
            await this._dbContext.SaveChangesAsync();

            return model;
        }

        public async Task Delete(string id)
        {
            var productEntity = this.GetProductAsDbEntity(id);

            this._dbContext.Products.Remove(productEntity);

            await this._dbContext.SaveChangesAsync();
        }

        public async Task Edit(ProductBaseInputModel model)
        {
            var category = this._categoryRepository.Get(model.CategoryId);

            if (category == null)
            {
                throw new ArgumentException("Inavalid Category Id.");
            }
            
            var productEntity = this.GetProductAsDbEntity(((ProductEditInputModel)model).Id);

            productEntity.Name = model.Name;
            productEntity.Description = model.Description;
            productEntity.Price = model.Price;
            productEntity.CoverUrl = model.CoverUrl;
            productEntity.CategoryId = model.CategoryId;

            this._dbContext.Products.Update(productEntity);
            await this._dbContext.SaveChangesAsync();
        }

        private Product GetProductAsDbEntity(string id)
        {
            var productEntity = this._dbContext.Products.FirstOrDefault(product => product.Id == id);

            if (productEntity == null)
            {
                throw new ArgumentException("Invalid product id.");
            }

            return productEntity;
        }
    }
}