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
    }
}