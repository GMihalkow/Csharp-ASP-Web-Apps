using System;

namespace MySocial.Data.Models
{
    public class Comment : BaseEntity<string>
    {
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public DateTime CommentedOn { get; set; }
    }
}