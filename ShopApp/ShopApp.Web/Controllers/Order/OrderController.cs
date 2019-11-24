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

        // TODO [GM]: Extract to API controller?
        [HttpPost]
        public async Task<string> Checkout(string products)
        {
            return await this.orderService.Checkout(products);
        }

        public async Task Cancel(string id)
        {
            await this.orderService.CancelOrder(id);
        }
    }
}