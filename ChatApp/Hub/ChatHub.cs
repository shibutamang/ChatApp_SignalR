using ChatApp.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatApp
{
    [Authorize]
    public class ChatHub : Hub
    {
        //server methods
        public void SendChatMessage(string who, string message)
        {
            var name = Context.User.Identity.Name;
            using (var db = new ApplicationDbContext())
            {
                var user = db.ChatUsers.Find(who);
                if (user == null)
                {
                    //clients methods
                    Clients.Caller.showErrorMessage("Could not find that user.");
                }
                else
                {
                    db.Entry(user)
                        .Collection(u => u.Connections)
                        .Query()
                        .Where(c => c.Connected == true)
                        .Load();

                    if (user.Connections == null)
                    {
                        Clients.Caller.showErrorMessage("The user is no longer connected.");
                    }
                    else
                    {
                        foreach (var connection in user.Connections)
                        {
                            //client methods
                            Clients.Client(connection.ConnectionID)
                                .addChatMessage(name, message);
                        }

                        //storing chat messages to database
                        var chatMsg = new ChatMessages() {Message = message, SentTo = user.UserName, SentFrom = name, SentOn = DateTime.UtcNow};
                        db.ChatMessages.Add(chatMsg);
                        db.SaveChanges();
                    }
                }
            }
        }
 

        public override Task OnConnected()
        {
            IList<ChatUserViewModel> users = new List<ChatUserViewModel>();

            using (var _db = new ApplicationDbContext())
            {
                var currentUser = HttpContext.Current.User.Identity.Name;

                var connectedUsers = _db.Connections.Where(u => u.Connected == true && u.ChatUser.UserName != currentUser)
                                        .Select(c => new { c.ChatUser.UserName }).GroupBy(x => x.UserName).ToList();

                var _chatusers = _db.ChatUsers.AsNoTracking().AsQueryable().Where(x => x.UserName != currentUser).ToList();
                 

                foreach (var user in _chatusers)
                {
                    var activeCount = user.Connections.Where(c => c.Connected == true).Count();
                    users.Add(new ChatUserViewModel() { UserName = user.UserName, IsActive = (activeCount > 0) ? true : false });
                }

                //Clients.All.getConnectedUsers(connectedUsers);
                //Clients.All.refreshUsers(users);
            }


            var name = Context.User.Identity.Name;
            using (var db = new ApplicationDbContext())
            {
                var user = db.ChatUsers
                    .Include(u => u.Connections)
                    .SingleOrDefault(u => u.UserName == name);

                if (user == null)
                {
                    user = new ChatUser
                    {
                        UserName = name,
                        Connections = new List<Connection>()
                    };
                    db.ChatUsers.Add(user);
                } 

                user.Connections.Add(new Connection
                {
                    ConnectionID = Context.ConnectionId,
                    UserAgent = Context.Request.Headers["User-Agent"],
                    Connected = true
                });
                db.SaveChanges();

                //refresh user list
                Clients.All.refreshUsers(users);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            using (var db = new ApplicationDbContext())
            {
                var connection = db.Connections.Find(Context.ConnectionId);
                connection.Connected = false;
                db.SaveChanges();
            }
            return base.OnDisconnected(stopCalled);
        }

        public IEnumerable<ChatUserViewModel> UpdateUsers()
        {
            var currentUser = HttpContext.Current.User.Identity.Name;
            IList<ChatUserViewModel> users = new List<ChatUserViewModel>();

            using (var _db = new ApplicationDbContext())
            {
                var _chatusers = _db.ChatUsers.AsNoTracking().AsQueryable().Where(x => x.UserName != currentUser).ToList();
                 

                foreach (var user in _chatusers)
                {
                    var activeCount = user.Connections.Where(c => c.Connected == true).Count();
                    users.Add(new ChatUserViewModel() { UserName = user.UserName, IsActive = (activeCount > 0) ? true : false });
                }
            }

            return users;
        }
    }
}