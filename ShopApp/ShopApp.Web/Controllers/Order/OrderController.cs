using ShopApp.Web.Constants;
using ShopApp.Web.Services.Order.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Order
{
    public class OrderController : Controller
    {
        public readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task Checkout(string products)
        {
            await this.orderService.Checkout(products);
        }

        [Authorize]
        public async Task Cancel(string id)
        {
            await this.orderService.CancelOrder(id);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public ActionResult All()
        {


            return this.View();
        }
    }
}