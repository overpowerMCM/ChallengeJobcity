using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ChallengeBackend.Sources
{
    public class RabbitMQConsumer
    {

        public event Action<string> RegisterMessaging;

        private IModel _channel;
        ConnectionFactory connectionFactory;
        IConnection _connection;
        public RabbitMQConsumer(string queue)
        {
            connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = connectionFactory.CreateConnection();

            _channel = _connection.CreateModel(); 
                
            _channel.QueueDeclare(queue, false, false, false, null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnReceived;
            _channel.BasicConsume(queue, true, consumer);

        }

        private void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            if (RegisterMessaging != null)
                RegisterMessaging.Invoke(Encoding.UTF8.GetString(e.Body));
        }


    }
}