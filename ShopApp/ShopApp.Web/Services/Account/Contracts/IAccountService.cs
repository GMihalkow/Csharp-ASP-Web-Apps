using ShopApp.Web.Models;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Account.Contracts
{
    public interface IAccountService
    {
        Task Register(RegisterInputModel model);

        Task Login(LoginInputModel model);

        void Logout();
    }
}