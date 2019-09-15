using ShopApp.Models;
using ShopApp.Web.Models;

namespace ShopApp.Web
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public ProductViewModel Product { get; set; }
        public string UserId { get; set; }
        public string User { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice => this.Quantity * this.Product.Price;
    }
}