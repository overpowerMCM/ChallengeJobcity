using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockBot.Sources
{
    /// <summary>
    /// Handles the csv logic that is implemented in this project
    /// </summary>
    public class ChallengeCSVStockManager : CSVStockManager
    {

        public ChallengeCSVStockManager()
        {
            CSVProvider = new StooqCSVProvider();
        }

        /// <summary>
        /// Process the stock code
        /// </summary>
        /// <param name="code">the code</param>
        public async void ProcessStockRequest(string code)
        {
            List<List<string>> values = await GetParsedCSV(code);
            if (values != null && values.Count > 0)
                SendToRabbit(values[1][0], values[1][6]);
        }

        /// <summary>
        /// formats the message to be published in rabbitmq
        /// </summary>
        /// <param name="code">the stock code</param>
        /// <param name="value">the value</param>
        void SendToRabbit(string code, string value)
        {
            RabbitMQPublisher publisher = new RabbitMQPublisher();
            publisher.SendToQueue("cola1", string.Format("{0} quote is ${1} per share.", code, value));
        }
    }
}