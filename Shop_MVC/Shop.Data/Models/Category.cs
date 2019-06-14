using System;
using System.Collections.Generic;

namespace Shop.Data.Models
{
    public class Category : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public ShopUser User { get; set; }
    }
}