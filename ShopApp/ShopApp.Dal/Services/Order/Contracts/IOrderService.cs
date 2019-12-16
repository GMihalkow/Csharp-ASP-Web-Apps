using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Dal.Services.Order.Contracts
{
    public interface IOrderService
    {
        ShopApp.Models.Order Get(string id);

        Task CancelOrder(string orderId);
        
        Task<string> Checkout(string ordersJson, string loggedInUserId);

        Task SendOrder(string orderId);

        Task CompleteOrder(string orderId);

        IEnumerable<OrderViewModel> GetOrders();
    }
}