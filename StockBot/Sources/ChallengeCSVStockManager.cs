using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockBot.Sources
{
    public class ChallengeCSVStockManager : CSVStockManager
    {

        public ChallengeCSVStockManager()
        {
            CSVProvider = new StooqCSVProvider();
        }

        public async void ProcessStockRequest(string code)
        {
            List<List<string>> values = await GetParsedCSV(code);
            if (values != null && values.Count > 0)
                SendToRabbit(values[1][0], values[1][6]);
        }

        void SendToRabbit(string code, string value)
        {
            RabbitMQPublisher publisher = new RabbitMQPublisher();
            publisher.SendToQueue("cola1", string.Format("{0} quote is ${1} per share.", code, value));
        }
    }
}