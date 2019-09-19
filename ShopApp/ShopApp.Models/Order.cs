using System;

namespace ShopApp.Models
{
    public class Order : BaseEntity<string>
    {
        public string UserId { get; set; }
        public virtual ShopUser User { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime OrderedOn { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => (decimal)(this.Product.Price * this.Quantity);
        public OrderStatus Status { get; set; }
    }
}