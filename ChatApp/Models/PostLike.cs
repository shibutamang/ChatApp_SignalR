using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    [Table("PostLike")]
    public class PostLike
    {
        [Key]
        public int LikeId { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }
    }
 
}