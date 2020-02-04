using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShopApp.Web.Constants;
using ShopApp.Web.Services.Role.Contracts;

namespace ShopApp.Web.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public async Task SeedRoles()
        {
            var userRoleExists = await this._roleManager.RoleExistsAsync(RolesConstants.User);
            if (!userRoleExists)
            {
                await this._roleManager.CreateAsync(new IdentityRole {Name = RolesConstants.User});
            }

            var adminRoleExists = await this._roleManager.RoleExistsAsync(RolesConstants.Administrator);
            if (!adminRoleExists)
            {
                await this._roleManager.CreateAsync(new IdentityRole {Name = RolesConstants.Administrator});
            }
        }
    }
}