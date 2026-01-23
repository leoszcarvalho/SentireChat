using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Models
{
    public class MessageItem
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int From { get; set; }          // 0 cliente / 1 bot / 2 humano (pelo seu print)
        public string? Text { get; set; }
        public string? MediaUrl { get; set; }
        public DateTime TimestampUtc { get; set; }
    }

}
