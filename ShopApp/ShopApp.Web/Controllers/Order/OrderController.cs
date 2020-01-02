using ShopApp.Dal.Services.Order.Contracts;
using ShopApp.Web.Constants;
using System;
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
            var orderUserId = this.orderService.Get(id).UserId;

            if (this.User.IsInRole(RolesConstants.Administrator) || orderUserId != this.LoggedUserId)
            {
                throw new InvalidOperationException("Invalid Order ID.");
            }

            await this.orderService.CancelOrder(id);
        }

        [HttpPost]
        public async Task<JsonResult> Checkout(string products)
        {
            return this.Json(await this.orderService.Checkout(products, this.LoggedUserId));
        }
    }
} 