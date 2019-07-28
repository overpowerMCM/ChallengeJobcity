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
        public override Task OnConnected()
        {
            MessengerRabbit.RegisterMessaging += OnBroadcast;
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            MessengerRabbit.RegisterMessaging -= OnBroadcast;
            return base.OnDisconnected(stopCalled);
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
                MessageSender.Instance.Post("https://localhost:44395/api/messages",  msg);
            }
            else
            {
                string[] split = sender.Split('@');
                string sendMessage = string.Format("{0}: {1}", split[0] ?? "Sender", msg);
                OnBroadcast(sendMessage);
            }
        }
        /*
        public void StartListeningMessages()
        {
            string cola = "cola1";
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using ( var connection = factory.CreateConnection() )
            {
                using ( var channel = connection.CreateModel() )
                {
                    channel.QueueDeclare(cola, false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(cola, true, consumer);
                    

                }
            }

        }*/
    }
}