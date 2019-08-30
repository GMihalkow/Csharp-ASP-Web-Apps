using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp.Web.Utilities
{
	public class ShopUserManager : UserManager<ShopUser>
	{
		public ShopUserManager(IUserStore<ShopUser> store) : base(store)
		{
		}

		public static ShopUserManager Create(IdentityFactoryOptions<ShopUserManager> options, IOwinContext context)
		{
			var manager = new ShopUserManager(new UserStore<ShopUser>(context.Get<ShopAppDbContext>()));

			// Configure validation logic for usernames
			manager.UserValidator = new ShopValidator(manager)
			{
				RequireUniqueEmail = true,
				AllowOnlyAlphanumericUserNames = false
			};

			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequireDigit = false,
				RequireLowercase = false,
				RequireUppercase = false,
				RequiredLength = 6
			};

			return manager;
		}
	}
}