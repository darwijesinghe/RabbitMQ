namespace RabbitMQ.Processor.Services.Interfaces
{
    /// <summary>
    /// Defines a contract for consuming messages from a message broker (e.g., RabbitMQ).
    /// </summary>
    public interface IMessageConsumer
    {
        /// <summary>
        /// Starts consuming messages from the configured queue using a direct exchange,
        /// where routing is based on a specific routing key.
        /// </summary>
        void DirectConsuming();

        /// <summary>
        /// Starts consuming messages from all queues bound to a fanout exchange,
        /// where messages are broadcast to all connected consumers.
        /// </summary>
        void FanoutConsuming();
    }
}
