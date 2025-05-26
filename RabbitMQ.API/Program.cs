using RabbitMQ.Shared.Classes;
using RabbitMQ.Shared.Interfaces;
using RabbitMQ.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ settings
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));

// RabbitMQ connection
builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

// RabbitMQ publisher
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
