using System;
using ShopApp.Models;
using System.Collections.Generic;

namespace ShopApp.Dal
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegisteredOn { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}