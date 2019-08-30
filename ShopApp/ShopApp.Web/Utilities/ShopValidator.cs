using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ShopApp.Models;

namespace ShopApp.Web.Utilities
{
	public class ShopValidator : UserValidator<ShopUser>
	{
		public ShopValidator(UserManager<ShopUser, string> manager) : base(manager)
		{
		}

		public override async Task<IdentityResult> ValidateAsync(ShopUser item)
		{
			await base.ValidateAsync(item);

			// TODO [GM]: custom validation logic here

			return IdentityResult.Success;
		}
	}
}