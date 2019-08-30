using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ShopApp.Models;

namespace ShopApp.Web.Utilities
{
	public class ShopSignInManager : SignInManager<ShopUser, string>
	{
		public ShopSignInManager(UserManager<ShopUser, string> userManager, IAuthenticationManager authenticationManager) 
			: base(userManager, authenticationManager)
		{
		}

		public static ShopSignInManager Create(IdentityFactoryOptions<ShopSignInManager> options, IOwinContext context)
		{
			return new ShopSignInManager(context.GetUserManager<ShopUserManager>(), context.Authentication);
		}
	}
}