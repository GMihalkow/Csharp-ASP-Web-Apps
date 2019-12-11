using ShopApp.Web.Constants;
using ShopApp.Web.Services.Order.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopApp.Web.Controllers.Api
{
    public class OrderApiController : BaseApiController
    {
        private readonly IOrderService orderService;

        public OrderApiController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        [Route(template: "api/OrderApi/GetOrders")]
        public JsonResult<List<OrderViewModel>> GetOrders()
        {
            List<OrderViewModel> orders = this.orderService.GetOrders().ToList();

            return this.Json(orders);
        }

        [Authorize]
        [HttpPost]
        [Route(template: "api/OrderApi/Checkout")]
        public async Task<JsonResult<string>> Checkout()
        {
            var products = HttpContext.Current.Request.Form["products"];

            return this.Json(await this.orderService.Checkout(products));
        }
    }
}