using System.Collections.Generic;

namespace SimpleShop.Data.Models
{
    public class Category : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Descritpion { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}