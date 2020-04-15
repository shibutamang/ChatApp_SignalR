using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Contents { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedOn { get; set; }
        public virtual ICollection<PostLike> PostLike { get; set; }
    }

    public class PostViewModel
    { 
        public int PostId { get; set; }
        public string Contents { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedOn { get; set; }
        public string UserName { get; set; }
        public int? LikesCount { get; set; }
    }
}