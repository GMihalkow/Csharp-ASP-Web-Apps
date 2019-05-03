using System;
using System.Collections.Generic;

namespace MySocial.Data.Models
{
    public class Post : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public bool IsPublic { get; set; }

        public int Shares { get; set; }

        public DateTime PostedOn { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}