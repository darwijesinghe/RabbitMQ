using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Processor;
using RabbitMQ.Processor.Services.Classes;
using RabbitMQ.Processor.Services.Interfaces;
using RabbitMQ.Shared.Classes;
using RabbitMQ.Shared.Interfaces;
using RabbitMQ.Shared.Settings;

class Program
{
    static void Main(string[] args)
    {
        // Adding services
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Register logging service
                services.AddLogging();

                // RabbitMQ settings
                services.Configure<RabbitMqSettings>(context.Configuration.GetSection("RabbitMq"));

                // RabbitMQ connection
                services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

                // RabbitMQ consumer
                services.AddSingleton<IMessageConsumer, MessageConsumer>();

            })
            .Build();

        // Automatically resolve and inject any dependencies required by the Startup class's constructor.
        // This helps in creating instances of classes that have dependencies registered in the DI container.
        var service = ActivatorUtilities.CreateInstance<Startup>(host.Services);
        service.Run();
    }
}