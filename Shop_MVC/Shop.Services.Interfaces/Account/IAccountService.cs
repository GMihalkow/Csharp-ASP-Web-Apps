using Shop.ViewModels.Interfaces.Account;

namespace Shop.Services.Interfaces.Account
{
    public interface IAccountService
    {
        void RegisterUser(IRegisterInputModel model);
    }
}