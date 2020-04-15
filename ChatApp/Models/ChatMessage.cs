using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class ChatMessages
    {
        [Key]
        public int ChatMessageId { get; set; }
        public string Message { get; set; }
        public string SentFrom{ get; set; }
        public string SentTo { get; set; }
        public DateTime SentOn { get; set; }
    }
}