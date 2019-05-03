using System;

namespace MySocial.Data.Models
{
    public class Message : BaseEntity<string>
    {
        public DateTime SentOn { get; set; }

        public string Content { get; set; }
            
        public string ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}