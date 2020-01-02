using ShopApp.Dal.Models.Roles;
using ShopApp.Dal.Services.Role.Contracts;
using ShopApp.Data;
using System;
using System.Linq;

namespace ShopApp.Dal.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly ShopAppDbContext dbContext;

        public RoleService(ShopAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ShopRole Get(string roleId)
        {
            var role = this.dbContext.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role == null) { throw new InvalidOperationException("Invalid role id."); }

            var shopRole = new ShopRole
            {
                Id = role.Id,
                Name = role.Name
            };

            return shopRole;
        }

        public ShopRole GetUserRole(string userId)
        {
            var role = this.dbContext.Roles.FirstOrDefault(r => r.Users.Any(u => u.UserId == userId));

            if (role == null) { throw new InvalidOperationException("Invalid role or user id."); }

            var shopRole = new ShopRole
            {
                Id = role.Id,
                Name = role.Name
            };

            return shopRole;
        }
    }
}