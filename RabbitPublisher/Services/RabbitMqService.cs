using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitSender.Interfaces;
using RabbitSender.Model;

namespace RabbitSender.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly AppSettings _settings;

    [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
    public RabbitMqService(IOptions<AppSettings> options)
    {
        _settings = options.Value;
    }

    public IConnection CreateChannel()
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(_settings.RabbitMq.RabbitUri),
            ClientProvidedName = "Rabbit Publisher App"
        };

        var channel = factory.CreateConnection();
        return channel;
    }
}