using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace StockBot.Sources
{
    public class RabbitMQPublisher
    {
        public void SendToQueue(string queue, string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    //string cola = "cola1";
                    channel.QueueDeclare(queue, false, false, false, null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", queue, null, body);
                }
            }
        }
    }
}