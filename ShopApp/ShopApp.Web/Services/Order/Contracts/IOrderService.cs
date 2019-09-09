using System.Threading.Tasks;

namespace ShopApp.Web.Services.Order.Contracts
{
    public interface IOrderService
    {
        Task Checkout(string ordersJson);
    }
}