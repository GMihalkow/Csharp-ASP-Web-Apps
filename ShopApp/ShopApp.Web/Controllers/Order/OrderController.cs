using ShopApp.Web.Services.Order.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        public readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task Cancel(string id)
        {
            await this.orderService.CancelOrder(id);
        }
    }
} 