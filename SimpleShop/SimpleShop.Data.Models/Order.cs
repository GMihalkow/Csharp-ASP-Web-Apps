using System;

namespace SimpleShop.Data.Models
{
    public class Order : BaseEntity<string>
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public ShopUser User { get; set; }

        public DateTime OrderedOn { get; set; }

        public int Quantity { get; set; }
    }
}