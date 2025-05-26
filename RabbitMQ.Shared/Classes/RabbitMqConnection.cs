using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Shared.Interfaces;
using RabbitMQ.Shared.Settings;

namespace RabbitMQ.Shared.Classes
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        // Connection instance
        private readonly IConnection _connection;

        public RabbitMqConnection(IOptions<RabbitMqSettings> options)
        {
            var factory = new ConnectionFactory
            {
                HostName = options.Value.HostName,
                UserName = options.Value.UserName,
                Password = options.Value.Password
            };

            _connection = factory.CreateConnection();
        }

        /// <inheritdoc/>
        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }
    }
}
