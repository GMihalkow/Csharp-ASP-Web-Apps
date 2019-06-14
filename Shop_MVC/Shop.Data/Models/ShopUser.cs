using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shop.Data.Models
{
    public class ShopUser : IdentityUser
    {
        public int Age { get; set; }

        public DateTime RegisteredOn { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}