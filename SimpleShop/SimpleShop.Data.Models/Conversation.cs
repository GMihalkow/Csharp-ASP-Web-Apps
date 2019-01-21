using System;
using System.Collections.Generic;

namespace SimpleShop.Data.Models
{
    public class Conversation : BaseEntity<string>
    {
        public ShopUser User1 { get; set; }

        public string User1Id { get; set; }

        public ShopUser User2 { get; set; }

        public string User2Id { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public DateTime StartedOn { get; set; }
    }
}