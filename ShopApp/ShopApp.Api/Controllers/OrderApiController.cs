using ShopApp.Dal.Services.Order.Contracts;
//using ShopApp.Web.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopApp.Api.Controllers
{
    public class OrderApiController : BaseApiController
    {
        private readonly IOrderService orderService;

        public OrderApiController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        //[Authorize(Roles = RolesConstants.Administrator)]
        [Route(template: "api/OrderApi/GetOrders")]
        public JsonResult<List<OrderViewModel>> GetOrders()
        {
            List<OrderViewModel> orders = this.orderService.GetOrders().ToList();

            return this.Json(orders);
        }
    }
}