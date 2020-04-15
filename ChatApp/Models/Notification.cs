using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    [Table("Notification")]
    public class Notification
    { 
        [Key]
        public int? NotificationId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string LinkUrl { get; set; }
        public string SentTo { get; set; }
        public DateTime? SentDate { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsReminder { get; set; }
        public string Code { get; set; }
    }
}