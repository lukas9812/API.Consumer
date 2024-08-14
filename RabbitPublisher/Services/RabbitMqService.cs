using RabbitMQ.Client;
using RabbitSender.Interfaces;

namespace RabbitSender.Services;

public class RabbitMqService : IRabbitMqService
{
    public IConnection CreateChannel()
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672"),
            ClientProvidedName = "Rabbit Publisher App"
        };

        var channel = factory.CreateConnection();
        return channel;
    }
}