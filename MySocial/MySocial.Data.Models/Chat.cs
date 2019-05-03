using System;
using System.Collections.Generic;

namespace MySocial.Data.Models
{
    public class Chat : BaseEntity<string>
    {
        public DateTime StartedOn { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public string RecieverId { get; set; }

        public User Reciever { get; set; }
    }
}