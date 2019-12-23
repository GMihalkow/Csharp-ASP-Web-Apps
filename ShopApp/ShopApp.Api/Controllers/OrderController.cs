using ShopApp.Dal.Services.Order.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopApp.Api.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [Route(template: "api/Order/GetOrders")]
        public JsonResult<List<OrderViewModel>> GetOrders()
        {
            List<OrderViewModel> orders = this.orderService.GetOrders().ToList();

            return this.Json(orders);
        }
    }
}