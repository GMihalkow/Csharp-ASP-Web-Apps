using System.Collections.Generic;

namespace SimpleShop.Data.Models
{
    public class Product : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public Category Category { get; set; }

        public string CategoryId { get; set; }
    }
}