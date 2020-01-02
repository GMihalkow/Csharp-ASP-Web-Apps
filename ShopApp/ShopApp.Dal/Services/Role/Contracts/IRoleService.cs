using ShopApp.Dal.Models.Roles;

namespace ShopApp.Dal.Services.Role.Contracts
{
    public interface IRoleService
    {
        ShopRole Get(string roleId);

        ShopRole GetUserRole(string userId);
    }
}