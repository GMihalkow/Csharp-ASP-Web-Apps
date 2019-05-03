using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MySocial.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string HomeTown { get; set; }

        public string Education { get; set; }

        public int Age { get; set; }

        public string ProfilePictureUrl { get; set; }

        public DateTime JoinedOn { get; set; }

        public DateTime LastActiveOn { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Chat> Chats { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<User> Friends { get; set; }
    }
}