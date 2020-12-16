using Microsoft.AspNetCore.SignalR;
using SignalR_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Server.Hubs
{
    public class NotificationHub:Hub
    {
        public Task SendMessage(Message message)
        {
            Debug.WriteLine("******************* Client Connection Id **********************");
            Debug.WriteLine(Context.ConnectionId);
            Debug.WriteLine("***************************************************************");
            return Clients.Others.SendAsync("Send", message);
        }
    }
}
