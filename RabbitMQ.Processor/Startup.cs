using RabbitMQ.Processor.Services.Interfaces;

namespace RabbitMQ.Processor
{
    public class Startup
    {
        // Services
        private readonly IMessageConsumer _consumer;

        public Startup(IMessageConsumer consumer)
        {
            _consumer = consumer;
        }

        public void Run()
        {
            // NOTE: Use one of these at a time to consume the messages

            // starts the consuming direct published messages
            //_consumer.DirectConsuming();

            // starts the consuming fanout published messages
            //_consumer.FanoutConsuming();
        }
    }
}
