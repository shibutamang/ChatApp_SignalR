using ChatApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        public ActionResult Index()
        {
            using (ApplicationDbContext _context = new ApplicationDbContext()) {
                var currentUser = User.Identity.Name;
                var _chatusers = _context.ChatUsers.AsNoTracking().AsQueryable().Where(x => x.UserName != currentUser).ToList();
                IList<ChatUserViewModel> chatUserVM = new List<ChatUserViewModel>();

                foreach (var user in _chatusers)
                {
                    var activeCount = user.Connections.Where(c => c.Connected == true).Count();
                    chatUserVM.Add(new ChatUserViewModel() { UserName = user.UserName, IsActive = (activeCount > 0) ? true : false });
                }

                ViewBag.users = chatUserVM;
            }
            return View();
        }

      
    }
}