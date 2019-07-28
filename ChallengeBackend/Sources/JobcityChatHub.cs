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
    public class JobcityChatHub : Hub
    {
        RabbitMQConsumer _consumer;

        public override Task OnConnected()
        {
            _consumer = new RabbitMQConsumer("cola1");
            _consumer.RegisterMessaging += OnBroadcast;

            return base.OnConnected();
        }


        private void OnBroadcast(string sendMessage)
        {
            Clients.All.SendMessage(sendMessage);
        }
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
                string[] split = sender.Split('@');
                string sendMessage = string.Format("{0}: {1}", split[0] ?? "Sender", msg);
                OnBroadcast(sendMessage);
            }
        }

    }
}