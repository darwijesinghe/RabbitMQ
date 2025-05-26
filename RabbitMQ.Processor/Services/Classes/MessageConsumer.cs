using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Processor.Services.Interfaces;
using RabbitMQ.Shared.Interfaces;
using RabbitMQ.Shared.Settings;
using System.Text;

namespace RabbitMQ.Processor.Services.Classes
{
    /// <summary>
    /// Consumes messages from RabbitMQ.
    /// </summary>
    public class MessageConsumer : IMessageConsumer
    {
        // Services
        private readonly IRabbitMqConnection _connection;
        private readonly RabbitMqSettings _settings;

        public MessageConsumer(IRabbitMqConnection connection, IOptions<RabbitMqSettings> settings)
        {
            _connection = connection;
            _settings   = settings.Value;
        }

        /// <inheritdoc/>
        public void DirectConsuming()
        {
            try
            {
                // creates a new channel
                using var channel = _connection.CreateModel();

                // declares the queue to ensure it exists before consuming messages
                channel.QueueDeclare(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false);

                // creates a new consumer that will be used to receive messages from the queue
                // it is tied to the current channel
                var consumer = new EventingBasicConsumer(channel);

                // event handler that gets called when a new message is received from the queue
                consumer.Received += (model, ea) =>
                {
                    var body    = ea.Body.ToArray();                // retrieves the message body as a byte array
                    var message = Encoding.UTF8.GetString(body);    // decodes the message body as a UTF-8 string

                    // logs the message
                    Console.WriteLine($"Received: {message}");
                };

                // starts consuming messages from the specified queue using the given consumer
                channel.BasicConsume(queue: _settings.QueueName, autoAck: true, consumer: consumer);

                Console.WriteLine("Consuming messages. Press [enter] to exit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public void FanoutConsuming()
        {
            try
            {
                // creates a new channel
                using var channel = _connection.CreateModel();

                // declares a fanout exchange with the specified name to ensure it exists before consuming messages.
                // in a fanout exchange, messages are broadcast to all queues bound to the exchange, regardless of routing keys.
                channel.ExchangeDeclare(exchange: _settings.ExchangeName, type: ExchangeType.Fanout);

                // unique, non-durable, auto-delete queue per consumer
                var queueName = channel.QueueDeclare().QueueName;

                // binds the specified queue to the fanout exchange.
                // in a fanout exchange, the routing key is ignored, so it is left as an empty string.
                channel.QueueBind(queue: queueName, exchange: _settings.ExchangeName, routingKey: "");

                // creates a new consumer that will be used to receive messages from the queue
                // it is tied to the current channel
                var consumer = new EventingBasicConsumer(channel);

                // event handler that gets called when a new message is received from the queue
                consumer.Received += (model, ea) =>
                {
                    var body    = ea.Body.ToArray();                // retrieves the message body as a byte array
                    var message = Encoding.UTF8.GetString(body);    // decodes the message body as a UTF-8 string

                    // logs the message
                    Console.WriteLine($"Received: {message}");
                };

                // starts consuming messages from the specified queue using the given consumer
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.WriteLine("Consuming messages. Press [enter] to exit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
