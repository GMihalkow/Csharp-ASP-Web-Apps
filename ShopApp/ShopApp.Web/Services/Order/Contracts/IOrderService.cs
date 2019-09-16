using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Order.Contracts
{
    public interface IOrderService
    {
        Task CancelOrder(string orderId);

        Task Checkout(string ordersJson);

        IEnumerable<OrderViewModel> GetOrders();

        // TODO [GM]: Implement /Orders/All (admin orders management functionality)
    }
}