using ChatApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    { 
        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<Post> posts = null;
            IList<PostViewModel> postVM = new List<PostViewModel>();
            using (ApplicationDbContext _context = new ApplicationDbContext()) {
                var currentUser = User.Identity.Name;
                var users = _context.ChatUsers.AsNoTracking().Where(x => x.UserName != currentUser).ToList();
                //var result = _context.Connections.Where(u => u.Connected == true && u.ChatUser.UserName != currentUser).Select(c => new { c.ChatUser.UserName}).GroupBy(x =>x.UserName).ToList();
                ViewBag.users = users;

                posts = _context.Post.Include("PostLike").ToList();
              
                foreach(var post in posts)
                {
                    string username = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(post.PostedBy).UserName;
                    postVM.Add(new PostViewModel() { PostId = post.PostId, Contents = post.Contents, PostedBy = post.PostedBy, PostedOn = post.PostedOn, UserName = username, LikesCount = post.PostLike.Count() });
                }

            }
            return View(postVM);
        }

      
    }
}