using ShopApp.Models;
using ShopApp.Web.Models;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Account.Contracts
{
    public interface IAccountService
	{
        Task<ShopUser> GetUser(string username);

		Task Register(RegisterInputModel model);

		Task Login(LoginInputModel model);

		void Logout();

        Task<ProfileViewModel> GetProfileInfo(string username);
	}
}