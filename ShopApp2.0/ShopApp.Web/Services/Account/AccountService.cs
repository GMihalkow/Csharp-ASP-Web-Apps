using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Account.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShopApp.Web.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly ShopAppDbContext _dbContext;
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(ShopAppDbContext dbContext, UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public async Task Register(RegisterInputModel model)
        {
            await this.SeedRoles();
            
            ShopUser user = new ShopUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                RegisteredOn = DateTime.UtcNow,
                Email = model.Email,
                BirthDate = model.BirthDate
            };

            if (this._userManager.Users.Any(u => u.UserName == user.UserName)) { throw new InvalidOperationException("Username is already taken."); }
            else if (this._userManager.Users.Any(u => u.Email == user.Email)) { throw new InvalidOperationException("Email already exists."); }

            IdentityResult result = await this._userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to register user.");
            }

            // the first registered user is the administrator
            if (this._dbContext.Users.Count() == 1)
            {
                await this._userManager.AddToRoleAsync(user, RolesConstants.Administrator);
            }
            else if (this._dbContext.Users.Count() > 1)
            {
                await this._userManager.AddToRoleAsync(user, RolesConstants.User);
            }
            
            await this._signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);
        }

        public async Task Login(LoginInputModel model)
        {
            SignInResult signInResult = await this._signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (signInResult != SignInResult.Success) { throw new InvalidOperationException("Incorrect username or password."); }
        }

        public async Task Logout() => await this._signInManager.SignOutAsync();

        // TODO [GM]: Extract to RoleService?
        private async Task SeedRoles()
        {
            bool userRoleExists = await this._roleManager.RoleExistsAsync(RolesConstants.User);
            if (!userRoleExists)
            {
                await this._roleManager.CreateAsync(new IdentityRole { Name = RolesConstants.User });
            }

            bool adminRoleExists = await this._roleManager.RoleExistsAsync(RolesConstants.Administrator);
            if (!adminRoleExists)
            {
                await this._roleManager.CreateAsync(new IdentityRole { Name = RolesConstants.Administrator });
            }
        }
    }
}