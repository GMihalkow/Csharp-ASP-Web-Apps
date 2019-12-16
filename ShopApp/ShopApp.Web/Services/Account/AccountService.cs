using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ShopApp.Models;
using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Web.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShopApp.Web.Services.Account
{
    public class AccountService : IAccountService
    {
        // TODO [GM]: Remove not used packages from web project
        private ShopUserManager userManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ShopUserManager>(); }
        }

        private ShopSignInManager signInManager
        {
            get { return HttpContext.Current.GetOwinContext().Get<ShopSignInManager>(); }
        }

        private ShopRoleManager roleManager
        {
            get { return HttpContext.Current.GetOwinContext().Get<ShopRoleManager>(); }
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

            if (this.userManager.Users.Any(u => u.UserName == user.UserName)) { throw new InvalidOperationException("Username is already taken."); }
            else if (this.userManager.Users.Any(u => u.Email == user.Email)) { throw new InvalidOperationException("Email already exists."); }

            IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to register user.");
            }

            // the first registered user is the administrator
            //if (this.dbContext.Users.Count() == 1)
            //{
            //    await this.userManager.AddToRoleAsync(user.Id, RolesConstants.Administrator);
            //}
            //else if (this.dbContext.Users.Count() > 1)
            //{
            //    await this.userManager.AddToRoleAsync(user.Id, RolesConstants.User);
            //}

            await this.signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);
        }

        public async Task Login(LoginInputModel model)
        {
            SignInStatus signInResult = await this.signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (signInResult == SignInStatus.Failure) { throw new InvalidOperationException("Incorrect username or password."); }
        }

        public void Logout()
        {
            this.signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private async Task SeedRoles()
        {
            bool userRoleExists = await this.roleManager.RoleExistsAsync(RolesConstants.User);
            if (!userRoleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = RolesConstants.User });
            }

            bool adminRoleExists = await this.roleManager.RoleExistsAsync(RolesConstants.Administrator);
            if (!adminRoleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole { Name = RolesConstants.Administrator });
            }
        }
    }
}