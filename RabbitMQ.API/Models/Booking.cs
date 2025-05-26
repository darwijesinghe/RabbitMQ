namespace RabbitMQ.API.Models
{
    /// <summary>
    /// Represents the booking related properties.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Booking id.
        /// </summary>
        public Guid Id                         { get; set; }

        /// <summary>
        /// Passenger name.
        /// </summary>
        public required string PassengerName  { get; set; }

        /// <summary>
        /// Passport number.
        /// </summary>
        public required string PassportNumber { get; set; }

        /// <summary>
        /// Departure location.
        /// </summary>
        public required string From           { get; set; }

        /// <summary>
        /// Destination location.
        /// </summary>
        public required string To             { get; set; }

        public Booking()
        {
            Id = Guid.NewGuid();
        }
    }
}
