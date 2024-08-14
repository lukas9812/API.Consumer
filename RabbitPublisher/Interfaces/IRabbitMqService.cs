using RabbitMQ.Client;

namespace RabbitSender.Interfaces;

public interface IRabbitMqService
{
    IConnection CreateChannel();
}