using ShopApp.Dal.Services.User.Contracts;
using ShopApp.Data;
using ShopApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Dal.Services.User
{
    public class UserService : IUserService
    {
        private readonly ShopAppDbContext _dbContext;

        public UserService(ShopAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<ShopUser> GetAll() => this._dbContext.Users.ToList();

        public ShopUser GetUserByName(string username) => this._dbContext.Users.FirstOrDefault(u => u.UserName == username);

        public ShopUser GetUserById(string id) => this._dbContext.Users.FirstOrDefault(u => u.Id == id);
    }
}