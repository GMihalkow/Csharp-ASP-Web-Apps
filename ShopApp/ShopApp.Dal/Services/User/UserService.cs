using ShopApp.Dal.Models.User;
using ShopApp.Dal.Services.Role.Contracts;
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
        private readonly IRoleService roleService;

        public UserService(ShopAppDbContext dbContext, IRoleService roleService)
        {
            this.dbContext = dbContext;
            this.roleService = roleService;
        }

        public IEnumerable<ShopUser> GetAll() => this.dbContext.Users.ToList();

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
                Orders = userEntity.Orders?.Take(5),
                PhoneNumber = userEntity.PhoneNumber,
                RegisteredOn = userEntity.RegisteredOn
            };

            return profileModel;
        }

        public ShopUser GetUserByName(string username) => this.dbContext.Users.FirstOrDefault(u => u.UserName == username);

        public ShopUser GetUserById(string id) => this.dbContext.Users.FirstOrDefault(u => u.Id == id);

        public IEnumerable<UserViewModel> GetAllUsersViewModels()
        {
            var users = this.dbContext.Users.ToList();

            var outputModels = users
            .Select(eu => new UserViewModel
            {
                Id = eu.Id,
                Username = eu.UserName,
                Email = eu.Email,
                FirstName = eu.FirstName,
                LastName = eu.LastName,
                RegisteredOn = eu.RegisteredOn,
                RoleName = this.roleService.GetUserRole(eu.Id).Name
            })
            .ToList();

            return outputModels;
        }
    }
}