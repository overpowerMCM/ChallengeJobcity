﻿using Microsoft.VisualBasic.FileIO;
using RabbitMQ.Client;
using StockBot.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StockBot.Controllers
{
    public class MessagesController : ApiController
    {
        // POST api/values
        public void Post(HttpRequestMessage value)
        {

            var content = value.Content;
            string stock = content.ReadAsStringAsync().Result;

            BotProcessHelper helper = new BotProcessHelper();

            helper.ProcessStockRequest(stock);

            /*
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
            }*/
        }



    }
}
