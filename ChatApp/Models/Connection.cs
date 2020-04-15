
namespace ChatApp.Models
{
    public class Connection
    {
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
        public virtual ChatUser ChatUser { get; set; }
    }
}