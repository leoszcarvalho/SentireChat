using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Models
{
    public class ReplyRequest
    {
        public int ConversationId { get; set; }
        public string Text { get; set; } = "";
    }

}
