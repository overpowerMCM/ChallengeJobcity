using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace StockBot.Controllers
{
    public class MessagesController : ApiController
    {
        // POST api/values
        public void Post(HttpRequestMessage value)
        {

            var content = value.Content;
            string jsonContent = content.ReadAsStringAsync().Result;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using ( var connection = factory.CreateConnection() )
            {
                using ( var channel = connection.CreateModel() )
                {

                    string cola = "cola1";
                    channel.QueueDeclare(cola, false, false, false, null);

                    var body = Encoding.UTF8.GetBytes(jsonContent);

                    channel.BasicPublish("", cola, null, body);
                }
            }
        }

    }
}
