using SimpleShop.DataServices.Models.Interfaces.Account;

namespace SimpleShop.DataServices.Interfaces.Account
{
    public interface IAccountService
    {
        void RegisterUser(IRegisterInputModel model);

        void LoginUser(ILoginInputModel model);

        void LogoutUser();
    }
}