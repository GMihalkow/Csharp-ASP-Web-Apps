using Microsoft.AspNet.Identity.Owin;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Web.Utilities;
using System.Threading.Tasks;
using System.Web;
using ShopApp.Web.Models;
using System;
using ShopApp.Models;

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
			ShopUser user = new ShopUser
			{
				UserName = model.UserName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				RegisteredOn = DateTime.UtcNow,
				Email = model.Email,
				BirthDate = model.BirthDate
			};

			var result = await this.userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				throw new InvalidOperationException("Failed to register user.");
			}

			await this.signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);
			
		}
	}
}