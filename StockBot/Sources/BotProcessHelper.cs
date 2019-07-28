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
    public class BotProcessHelper
    {

        ICSVProvider _csvProvider;

        public ICSVProvider CSVProvider { get => _csvProvider ?? (_csvProvider = new DefaultCSVProvider()); set => _csvProvider = value; }

        public async void ProcessStockRequest( string code )
        {
            byte[] data = await CSVProvider.GetStockData(code);
            List<List<string>> values = ParseCSV( data );
            if( values != null )
                SendToRabbit(values[1][0], values[1][6]);
        }

        public List<List<string>> ParseCSV(byte[] csv)
        {
            List<List<string>> list = null;

            if (csv != null)
            {
                MemoryStream stream = new MemoryStream(csv);
                using (TextFieldParser parser = new TextFieldParser(stream))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        //Process row
                        if (list == null) list = new List<List<string>>();
                        List<string> element = new List<string>();
                        element.AddRange(parser.ReadFields());
                        list.Add(element);
                    }
                }
            }
            return list;
        }

        void SendToRabbit(string code, string value)
        {
            string message = string.Format("{0} quote is ${1} per share.", code, value);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    string cola = "cola1";
                    channel.QueueDeclare(cola, false, false, false, null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", cola, null, body);
                }
            }
        }
    }
}