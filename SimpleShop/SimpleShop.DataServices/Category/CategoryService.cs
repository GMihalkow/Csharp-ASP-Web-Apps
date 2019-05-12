using SimpleShop.DataServices.Interfaces.Account;
using SimpleShop.DataServices.Interfaces.Category;
using SimpleShop.DataServices.Interfaces.Db;
using SimpleShop.DataServices.Models.Interfaces.Category;
using System;
using System.Linq;
using System.Security.Claims;

namespace SimpleShop.DataServices.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbService dbService;
        private readonly IAccountService accountService;

        public CategoryService(IDbService dbService, IAccountService accountService)
        {
            this.dbService = dbService;
            this.accountService = accountService;
        }

        public void Create(ICategoryInputModel model, ClaimsPrincipal principal)
        {
            var categoryExists = this.dbService.DbContext.Categories.Any(c => c.Name == model.Name);
            if (categoryExists)
            {
                throw new InvalidOperationException("Category already exists!");
            }

            var author = this.accountService.GetUser(principal);

            var category = new SimpleShop.Data.Models.Category
            {
                Name = model.Name,
                Description = model.Description,
                User = author,
                UserId = author.Id
            };

            this.dbService.DbContext.Categories.Add(category);
            this.dbService.DbContext.SaveChanges();
        }
    }
}