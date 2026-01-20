using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Models
{
    public class MessageItem
    {
        public int From { get; set; } // 0 cliente | 1 bot/humano
        public string Text { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }

}
