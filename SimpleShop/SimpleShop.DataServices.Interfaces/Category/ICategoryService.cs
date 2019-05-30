using SimpleShop.DataServices.Models.Interfaces.Category;
using System.Collections.Generic;
using System.Security.Claims;

namespace SimpleShop.DataServices.Interfaces.Category
{
    public interface ICategoryService
    {
        void Create(ICategoryInputModel model, ClaimsPrincipal principal);

        IEnumerable<ICategoryViewModel> GetCategories();
    }
}