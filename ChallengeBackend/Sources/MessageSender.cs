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
    /// <summary>
    /// sends https post messages to an url
    /// </summary>
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

        /// <summary>
        /// Post a message to url
        /// </summary>
        /// <param name="url">the url</param>
        /// <param name="message">the post message</param>
        public void Post(string url, string message)
        {
            var content = new StringContent(message, Encoding.UTF8, "text/plain");
            _client.PostAsync(url, content);
        }
    }
}
