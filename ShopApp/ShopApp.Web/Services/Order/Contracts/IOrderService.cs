using System.Threading.Tasks;

namespace ShopApp.Web.Services.Order.Contracts
{
    public interface IOrderService
    {
        Task CancelOrder(string orderId);

        Task Checkout(string ordersJson);

        // TODO [GM]: Implement /Orders/All (admin orders management functionality)
    }
}