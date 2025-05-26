using Microsoft.AspNetCore.Mvc;
using RabbitMQ.API.Models;
using RabbitMQ.Shared.Interfaces;

namespace RabbitMQ.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        // Services
        private readonly IMessagePublisher _messagePublisher;

        public MessageController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        /// <summary>
        /// Publishes a booking message to the message broker.
        /// </summary>
        /// <param name="data">The booking data to be published.</param>
        /// <returns>
        /// A JSON result indicating the outcome of the publish operation.
        /// </returns>
        [HttpPost("publish-message")]
        public JsonResult PublishMessage([FromBody] Booking data)
        {
            try
            {
                // validations
                if (!ModelState.IsValid)
                    return new JsonResult(new { message = "Required data is not found." });

                // gets the published result
                var result = _messagePublisher.DirectPublish(data);
                if (result == null)
                    return new JsonResult(new { message = "OK." });
                else
                    return new JsonResult(new { message = result });
            }
            catch (Exception ex)
            {
                // returns the error
                return new JsonResult(new { ex.Message });
            }
        }

        /// <summary>
        /// Publishes the booking data to a RabbitMQ fanout exchange,
        /// broadcasting the message to all bound queues.
        /// </summary>
        /// <param name="data">The booking data to be published.</param>
        /// <returns>
        /// A JSON result indicating the outcome of the publish operation.
        /// </returns>
        [HttpPost("publish-fanout-message")]
        public JsonResult PublishFanoutMessage([FromBody] Booking data)
        {
            try
            {
                // validations
                if (!ModelState.IsValid)
                    return new JsonResult(new { message = "Required data is not found." });

                // gets the published result
                var result = _messagePublisher.FanoutPublish(data);
                if (result == null)
                    return new JsonResult(new { message = "OK." });
                else
                    return new JsonResult(new { message = result });
            }
            catch (Exception ex)
            {
                // returns the error
                return new JsonResult(new { ex.Message });
            }
        }
    }
}
