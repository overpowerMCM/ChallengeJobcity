using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChallengeBackend.Sources
{
    public class JobcityChatHub : Hub
    {
        public void ProcessMessage(string msg)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string sendMessage = string.Format("{0}: {1}", userName, msg);
            Clients.All.SendMessage(sendMessage);    
        }
    }
}