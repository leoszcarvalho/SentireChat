using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Models
{
    public class ConversationSummaryDto
    {
        public int Id { get; set; }
        public string? ClientNumber { get; set; }
        public string? ClientName { get; set; }
        public int Mode { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastMessageAtUtc { get; set; }
    }
}
