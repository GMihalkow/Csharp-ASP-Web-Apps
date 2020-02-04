using ShopApp.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ShopApp.Web.Services.Account.Contracts
{
    public interface IAccountService
    {
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string returnUrl);
    
        Task ExternalLogin();
        
        Task Register(RegisterInputModel model);

        Task Login(LoginInputModel model);

        Task Logout();
    }
}