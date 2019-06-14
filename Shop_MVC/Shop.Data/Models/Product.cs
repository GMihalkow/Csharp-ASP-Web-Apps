using System;

namespace Shop.Data.Models
{
    public class Product : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
