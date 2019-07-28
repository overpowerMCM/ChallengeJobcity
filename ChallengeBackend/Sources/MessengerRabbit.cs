using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ChallengeBackend.Sources
{
    public class MessengerRabbit //: DefaultBasicConsumer
    {
        private static MessengerRabbit _instance = null;
        public static MessengerRabbit Instance { get => _instance ?? (_instance = new MessengerRabbit()); }

        public static event Action<string> RegisterMessaging;

        private IModel _channel;
        ConnectionFactory connectionFactory;
        IConnection _connection;
        private MessengerRabbit()
        {
            connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            string cola = "cola1";
            _connection = connectionFactory.CreateConnection();

            _channel = _connection.CreateModel(); 
                
            _channel.QueueDeclare(cola, false, false, false, null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnReceived;
            _channel.BasicConsume(cola, true, consumer);

        }

        private void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            if (RegisterMessaging != null)
                RegisterMessaging.Invoke(Encoding.UTF8.GetString(e.Body));
        }

        public  void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            _channel.BasicAck(deliveryTag, false);

        }

    }
}