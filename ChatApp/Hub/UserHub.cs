using ChatApp.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatApp
{
    public class UserHub : Hub
    {
        public override Task OnConnected()
        { 
             
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        { 
            return base.OnDisconnected(stopCalled);
        }
    }
}