using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChallengeBackend.Sources
{
    /// <summary>
    /// custom SignalR Chat hub 
    /// </summary>
    public class JobcityChatHub : Hub
    {
        RabbitMQConsumer _consumer;

        public override Task OnConnected()
        {
            _consumer = new RabbitMQConsumer( Models.Helpers.DBContextHelper.Instance.Room.Title);
            _consumer.RegisterMessaging += OnBroadcast;

            return base.OnConnected();
        }

        /// <summary>
        /// Broadcast a message to all clients.
        /// </summary>
        /// <param name="sendMessage"></param>
        private void OnBroadcast(string sendMessage)
        {
            Clients.All.SendMessage(sendMessage);
        }

        /// <summary>
        /// Broadcast a message. it evaluate for stock related commands.
        /// </summary>
        /// <param name="sender">who is sending the message</param>
        /// <param name="msg">the message</param>
        public void Broadcast(string sender, string msg)
        {
            if (msg.Contains("/stock="))
            {
                // call bot
                string[] stockSplit = msg.Split('=');
                if( stockSplit[1].Length > 0 )
                    MessageSender.Instance.Post("https://localhost:44395/api/messages", stockSplit[1]);
            }
            else
            {
                if (!string.IsNullOrEmpty(msg)) return;
                string[] split = sender.Split('@');
                string sendMessage = string.Format("{0}: {1}", split[0] ?? "Sender", msg);

                ChallengeBackend.Models.Helpers.DBContextHelper.Instance.StoreMessage(msg);
                OnBroadcast(sendMessage);
            }
        }

    }
}