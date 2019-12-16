using ShopApp.Models;
using System.Collections.Generic;

namespace ShopApp.Dal.Services.User.Contracts
{
    public interface IUserService
    {
        ShopUser GetUserByName(string username);

        ShopUser GetUserById(string id);

        ProfileViewModel GetProfileInfo(string username);

        // TODO [GM]: Dedicated view model for GetUser/GetAll ?
        IEnumerable<ShopUser> GetAll();
    }
}