using System.Collections.Generic;

namespace SimpleShop.Data.Models
{
    public class Category : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public ShopUser User { get; set; }

        public string UserId { get; set; }
    }
}