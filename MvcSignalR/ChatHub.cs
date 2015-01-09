using Microsoft.AspNet.SignalR;
using SingnalRService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSignalR
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
            
            //using (SingnalRService.SingnalRContext context = new SingnalRService.SingnalRContext())
            //{
            //    context.Messages.Add(new Message()
            //    {
            //        Name = name,
            //        MessageStr = message
            //    });
            //    context.SaveChanges();
            //}
        }
    }
}