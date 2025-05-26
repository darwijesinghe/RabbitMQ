# RabbitMQMessagingApp

## Project Purpose
This is a basic messaging-based application using RabbitMQ. It demonstrates asynchronous communication between services using a .NET 8 REST API and a Console Application. The project includes message publishing and consumption using both direct and fanout exchanges. It serves as a practical example of working with RabbitMQ for decoupled, event-driven architecture in .NET applications.

## Contributors
Darshana Wijesinghe

## App Features
- Asynchronous communication using RabbitMQ as the message broker.
- Integration between a .NET 8 Web API and a Console Application for message publishing and consumption.
- Implements message routing using direct and fanout exchanges.
- Includes a docker-compose.yml file to easily spin up a RabbitMQ instance.

## Packages
- RabbitMQ.Client
- Newtonsoft.Json

## Usage
```json5
// Publish direct message

POST /api/message/publish-message

// Request Body

{
  "passengerName": "Nadeesha Wijesinghe",
  "passportNumber": "NH961062853",
  "from": "Sri Lanka",
  "to": "London, UK"
}
```
```json5
// Publish fanout message

POST /api/message/publish-fanout-message

// Request Body

{
  "passengerName": "Darshana Wijesinghe",
  "passportNumber": "NH961062853",
  "from": "Sri Lanka",
  "to": "London, UK"
}
```
## Support
Darshana Wijesinghe  
Email address - [dar.mail.work@gmail.com](mailto:dar.mail.work@gmail.com)  
Linkedin - [darwijesinghe](https://www.linkedin.com/in/darwijesinghe/)  
GitHub - [darwijesinghe](https://github.com/darwijesinghe)

## License
This project is licensed under the terms of the **MIT** license.