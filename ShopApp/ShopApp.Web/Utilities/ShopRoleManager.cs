using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopApp.Data;

namespace ShopApp.Web.Utilities
{
	public class ShopRoleManager : RoleManager<IdentityRole>
	{
		public ShopRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
		{
		}

		public static ShopRoleManager Create()
		{
			return new ShopRoleManager(new RoleStore<IdentityRole>(ShopAppDbContext.Create()));
		}
	}
}