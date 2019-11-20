using ShopApp.Web.Constants;
using ShopApp.Web.Services.Order.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Order
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

        [Authorize(Roles = RolesConstants.Administrator)]
        public ActionResult All()
        {
            List<OrderViewModel> orders = this.orderService.GetOrders().ToList();

            return this.View(orders);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Send(string id)
        {
            await this.orderService.SendOrder(id);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Complete(string id)
        {
            await this.orderService.CompleteOrder(id);
        }
    }
}