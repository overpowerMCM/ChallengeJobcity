using Microsoft.VisualBasic.FileIO;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StockBot.Sources
{
    public class CSVStockManager
    {

        protected ICSVProvider _csvProvider;
        protected ICSVParser _csvParser;

        public ICSVProvider CSVProvider { get => _csvProvider ?? (_csvProvider = new DefaultCSVProvider()); set => _csvProvider = value; }
        public ICSVParser CSVParser { get => _csvParser ?? (_csvParser = new DefaultCSVParser()); set => _csvParser = value; }

        public async Task<List<List<string>>> GetParsedCSV(string code )
        {
            byte[] data = await CSVProvider.GetStockData(code);
            return CSVParser.Parse(data);
        }
/*
        public async void ProcessStockRequest( string code )
        {
            byte[] data = await CSVProvider.GetStockData(code);
            List<List<string>> values = CSVParser.Parse( data );
            if( values != null && values.Count > 0)
                SendToRabbit(values[1][0], values[1][6]);
        }

        void SendToRabbit(string code, string value)
        {
            RabbitMQPublisher publisher = new RabbitMQPublisher();
            publisher.SendToQueue("cola1", string.Format("{0} quote is ${1} per share.", code, value));     
        }*/
    }
}