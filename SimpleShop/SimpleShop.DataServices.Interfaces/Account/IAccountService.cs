using SimpleShop.Data.Models;
using SimpleShop.DataServices.Models.Interfaces.Account;
using System.Security.Claims;

namespace SimpleShop.DataServices.Interfaces.Account
{
    public interface IAccountService
    {
        void RegisterUser(IRegisterInputModel model);

        void LoginUser(ILoginInputModel model);

        void LogoutUser();

        ShopUser GetUser(ClaimsPrincipal principal);
    }
}