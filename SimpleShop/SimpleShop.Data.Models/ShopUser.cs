using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SimpleShop.Data.Models
{
    public class ShopUser : IdentityUser<string>
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<Conversation> Conversations { get; set; }

        public DateTime RegisteredOn { get; set; }

        public decimal Wallet { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
