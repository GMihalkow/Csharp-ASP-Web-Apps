using Microsoft.AspNet.Identity.Owin;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Web.Utilities;
using System.Threading.Tasks;
using System.Web;
using ShopApp.Web.Models;
using System;
using ShopApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopApp.Data;
using System.Linq;
using ShopApp.Web.Constants;

namespace ShopApp.Web.Services.Account
{
    public class AccountService : IAccountService
	{
		private ShopUserManager userManager
		{
			get
			{
				return HttpContext.Current.GetOwinContext().GetUserManager<ShopUserManager>();
			}
		}

		private ShopAppDbContext dbContext
		{
			get
			{
				return HttpContext.Current.GetOwinContext().Get<ShopAppDbContext>();
			}
		}

		private ShopSignInManager signInManager
		{
			get
			{
				return HttpContext.Current.GetOwinContext().Get<ShopSignInManager>();
			}
		}

		private ShopRoleManager roleManager
		{
			get
			{
				return HttpContext.Current.GetOwinContext().Get<ShopRoleManager>();
			}
		}

		public async Task Register(RegisterInputModel model)
		{
			await this.SeedRoles();

			ShopUser user = new ShopUser
			{
				UserName = model.UserName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				RegisteredOn = DateTime.UtcNow,
				Email = model.Email,
				BirthDate = model.BirthDate
			};

			IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				throw new InvalidOperationException("Failed to register user.");
			}

			// the first registered user is the administrator
			if (this.dbContext.Users.Count() == 1)
			{
				await this.userManager.AddToRoleAsync(this.dbContext.Users.FirstOrDefault().Id, RolesConstants.Administrator);
			}
			else if (this.dbContext.Users.Count() > 1)
			{
				await this.userManager.AddToRoleAsync(this.dbContext.Users.LastOrDefault().Id, RolesConstants.User);
			}

			await this.signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

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

		public async Task Login(LoginInputModel model)
		{
			SignInStatus signInResult = await this.signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

			if (signInResult == SignInStatus.Failure)
			{
				throw new InvalidOperationException("Incorrect username or password.");
			}
		}

		public void Logout()
		{
			this.signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
		}

        public ShopUser GetUser(string username)
        {
            ShopUser user = this.dbContext.Users.FirstOrDefault(u => u.UserName == username);

            return user;
        }
    }
}