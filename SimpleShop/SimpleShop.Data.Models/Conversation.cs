using System;
using System.Collections.Generic;

namespace SimpleShop.Data.Models
{
    public class Conversation : BaseEntity<string>
    {
        public ShopUser Author { get; set; }

        public string AuthorId { get; set; }

        public ShopUser Reciever { get; set; }

        public string RecieverId { get; set; }
            
        public IEnumerable<Message> Messages { get; set; }

        public DateTime StartedOn { get; set; }
    }
}