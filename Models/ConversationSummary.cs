using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Models
{
    public class ConversationSummary
    {
        public int ConversationId { get; set; }
        public string ClientNumber { get; set; } = "";
        public string LastMessagePreview { get; set; } = "";
        public DateTime LastMessageAt { get; set; }
    }
}
