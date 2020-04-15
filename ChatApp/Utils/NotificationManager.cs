using ChatApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System; 
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChatApp
{
    public static class NotificationManager
    {
        //Get notified based on user id
        public static void NotifyUser(Notification notification)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();


            using (var db = new ApplicationDbContext())
            {
                //var userId = HttpContext.Current.User.Identity.GetUserId();
                var toUser = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(notification.SentTo);

                var user = db.ChatUsers.Find(toUser.UserName);

                if (user != null)
                {
                    db.Entry(user)
                       .Collection(u => u.Connections)
                       .Query()
                       .Where(c => c.Connected == true)
                       .Load();

                    foreach (var connection in user.Connections)
                    {
                        //client methods
                        hubContext.Clients.Client(connection.ConnectionID)
                            .getNotification(notification);
                    }
                }
            }
        }
    }
}