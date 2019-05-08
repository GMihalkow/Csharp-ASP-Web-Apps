using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SimpleShop.Data.Models
{
    public class ShopUser : IdentityUser
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string ProfilePicUrl { get; set; }

        public DateTime BirthDate { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<Conversation> StartedConversations { get; set; }

        public IEnumerable<Conversation> RecievedConversations { get; set; }

        public DateTime RegisteredOn { get; set; }
        
        public IEnumerable<Category> Categories { get; set; }
    }
}