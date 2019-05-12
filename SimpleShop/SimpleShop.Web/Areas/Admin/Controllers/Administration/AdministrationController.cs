using DataServices.Common;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.DataServices.Interfaces.Db;
using SimpleShop.Web.Controllers;
using SimpleShop.Web.Attributes.Authorize;

namespace SimpleShop.Web.Areas.Admin.Controllers.Administration
{
    [Area("Admin")]
    [AuthorizeRoles(new string[] { Role.Administrator, Role.Owner })]
    public class AdministrationController : BaseController
    {
        public AdministrationController(IDbService dbService) : base(dbService)
        {
        }

        public IActionResult CommandPanel()
        {
            return this.View();
        }
    }
}