using ShopApp.Web.Constants;
using ShopApp.Web.Controllers;
using ShopApp.Web.Services.Order.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RolesConstants.Administrator)]
    public class AdministrationController : BaseController
    {
        public readonly IOrderService orderService;

        public AdministrationController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public ActionResult Orders() => this.View();

        public async Task Send(string id) => await this.orderService.SendOrder(id);

        public async Task Complete(string id) => await this.orderService.CompleteOrder(id);

        public ActionResult Products() => this.View();
    }
}