using Microsoft.VisualBasic.FileIO;
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
            helper.CSVProvider = new JobcityCSVProvider();

            helper.ProcessStockRequest(stock);

        }



    }
}
