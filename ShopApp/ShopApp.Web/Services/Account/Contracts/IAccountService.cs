using ShopApp.Models;
using ShopApp.Web.Models;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Account.Contracts
{
	public interface IAccountService
	{
        ShopUser GetUser(string username);

		Task Register(RegisterInputModel model);

		Task Login(LoginInputModel model);

		void Logout();

        ProfileViewModel GetProfileInfo(string username);
	}
}