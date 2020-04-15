using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class ChatUser
    {
        [Key]
        public string UserName { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
    }

    public class ChatUserViewModel
    {
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}