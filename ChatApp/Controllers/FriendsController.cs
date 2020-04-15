using ChatApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

      
    }
}