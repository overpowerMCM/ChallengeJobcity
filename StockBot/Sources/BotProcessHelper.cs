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
        public async void ProcessStockRequest( string code )
        {
            byte[] file = await RetrieveCSV(code);
            List<List<string>> values = ParseCSV( file );
            if( values != null )
                SendToRabbit(code, values[1][6]);
        }

        async Task<byte[]> RetrieveCSV(string stockCode)
        {
            using (var client = new WebClient())
            {
                Uri uri = new Uri( string.Format("https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv​", stockCode) );
                var buffer = await client.DownloadDataTaskAsync(uri);
                return buffer;
            }
        }

        List<List<string>> ParseCSV(byte[] file)
        {
            MemoryStream stream = new MemoryStream(file);
            List<List<string>> list = null;
            using (TextFieldParser parser = new TextFieldParser(stream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                
                while (!parser.EndOfData)
                {
                    //Process row
                    if(list == null ) list = new List<List<string>>();
                    List<string> element = new List<string>();
                    element.AddRange(parser.ReadFields());
                    list.Add(element);
                }
            }

            return list;
        }

        void SendToRabbit(string code, string value)
        {
            string message = string.Format("{0} quote is ${1} per share.", code.ToUpper(), value);

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