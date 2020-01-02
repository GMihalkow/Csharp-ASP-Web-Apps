using ShopApp.Dal.Models.User;
using ShopApp.Models;
using System.Collections.Generic;

namespace ShopApp.Dal.Services.User.Contracts
{
    public interface IUserService
    {
        ShopUser GetUserByName(string username);

        ShopUser GetUserById(string id);

        ProfileViewModel GetProfileInfo(string username);

        IEnumerable<ShopUser> GetAll();

        IEnumerable<UserViewModel> GetAllUsersViewModels();
    }
}