using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Shared.Interfaces;
using RabbitMQ.Shared.Settings;
using System.Text;

namespace RabbitMQ.Shared.Classes
{
    /// <summary>
    /// Publishes messages to RabbitMQ.
    /// </summary>
    public class MessagePublisher : IMessagePublisher
    {
        // Services
        private readonly IRabbitMqConnection _connection;
        private readonly RabbitMqSettings    _settings;

        public MessagePublisher(IRabbitMqConnection connection, IOptions<RabbitMqSettings> settings)
        {
            _connection = connection;
            _settings   = settings.Value;
        }

        /// <inheritdoc/> 
        public string DirectPublish<T>(T message)
        {
            try
            {
                // creates a new channel
                using var channel = _connection.CreateModel();

                // declares the queue to ensure it exists before consuming messages.
                channel.QueueDeclare(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false);

                // prepares the message
                string jsonString = JsonConvert.SerializeObject(message);
                var body          = Encoding.UTF8.GetBytes(jsonString);

                // publishing the message
                channel.BasicPublish(exchange: "", routingKey: _settings.QueueName, basicProperties: null, body: body);

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <inheritdoc/> 
        public string FanoutPublish<T>(T message)
        {
            try
            {
                // creates a new channel
                using var channel = _connection.CreateModel();

                // declares a fanout exchange with the specified name to ensure it exists before consuming messages.
                // in a fanout exchange, messages are broadcast to all queues bound to the exchange, regardless of routing keys.
                channel.ExchangeDeclare(exchange: _settings.ExchangeName, type: ExchangeType.Fanout);

                // prepares the message
                string jsonString = JsonConvert.SerializeObject(message);
                var body          = Encoding.UTF8.GetBytes(jsonString);

                // publishing the message
                channel.BasicPublish(exchange: _settings.ExchangeName, routingKey: "", basicProperties: null, body: body);

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
