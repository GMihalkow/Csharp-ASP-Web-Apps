using ShopApp.Dal.Services.User.Contracts;
using ShopApp.Data;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Dal.Services.User
{
    public class UserService : IUserService
    {
        private readonly ShopAppDbContext dbContext;

        public UserService(ShopAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ShopUser> GetAll()
        {
            return this.dbContext.Users.ToList();
        }

        public ProfileViewModel GetProfileInfo(string username)
        {

            ShopUser userEntity = this.GetUserByName(username);

            if (userEntity == null) { throw new InvalidOperationException("Invalid user!"); }

            ProfileViewModel profileModel = new ProfileViewModel
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                BirthDate = userEntity.BirthDate,
                EmailAddress = userEntity.Email,
                Orders = userEntity.Orders.Take(5),
                PhoneNumber = userEntity.PhoneNumber,
                RegisteredOn = userEntity.RegisteredOn
            };

            return profileModel;
        }

        public ShopUser GetUserByName(string username)
        {
            return this.dbContext.Users.FirstOrDefault(u => u.UserName == username);
        }

        public ShopUser GetUserById(string id)
        {
            return this.dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}