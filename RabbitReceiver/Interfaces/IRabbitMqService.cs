using RabbitMQ.Client;

namespace RabbitReceiver1.Interfaces;

public interface IRabbitMqService
{
    IConnection? CreateChannel();
}