using ChatApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatApp
{
    public class NotificationHub : Hub
    {

         
        public IEnumerable<Notification> GetNotifications()
        { 
            IEnumerable<Notification> notifications = null;
            //var currentUser = Utils.GetCurrentUser();
            using (var _context = new ApplicationDbContext())
            {
                notifications = _context.Notification.ToList();
            }
            return notifications;
        }
         

        public override Task OnConnected()
        { 
            IEnumerable<Notification> notifications = null;
            using (var _context = new ApplicationDbContext())
            {
                notifications = _context.Notification.ToList();
            }
                Clients.All.getNotifications(notifications);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}