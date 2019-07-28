using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChallengeBackend.Sources
{
    public class MessageSender
    {
        private static MessageSender _instance;
        public static MessageSender Instance { get => _instance ?? (_instance = new MessageSender()); }

        HttpClient _client;

        private MessageSender()
        {
            _client = new HttpClient();

            _client.DefaultRequestHeaders.Accept.Clear();

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }

        public void Post(string url, string message)
        {
            var content = new StringContent(message, Encoding.UTF8, "text/plain");
            _client.PostAsync(url, content);
        }
    }
}
