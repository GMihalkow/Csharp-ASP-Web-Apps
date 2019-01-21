using System;

namespace SimpleShop.Data.Models
{
    public class Message : BaseEntity<string>
    {
        public string Description { get; set; }

        public Conversation Conversation { get; set; }

        public string ConversationId { get; set; }

        public DateTime SentOn { get; set; }

        public bool Seen { get; set; }
    }
}