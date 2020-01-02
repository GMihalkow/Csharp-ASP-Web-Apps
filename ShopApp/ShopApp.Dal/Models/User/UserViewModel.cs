using System;

namespace ShopApp.Dal.Models.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}