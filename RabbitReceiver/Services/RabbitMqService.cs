using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitReceiver1.Interfaces;
using RabbitReceiver1.Model;

namespace RabbitReceiver1.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly AppSettings _settings;
    private readonly ILogger<RabbitMqService> _logger;

    [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
    public RabbitMqService(IOptions<AppSettings> options, ILogger<RabbitMqService> logger)
    {
        _logger = logger;
        _settings = options.Value;
    }

    public IConnection? CreateChannel()
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(_settings.RabbitMq!.RabbitUri),
            ClientProvidedName = "Rabbit Receiver App"
        };

        var channel = factory.CreateConnection();
        
        if(channel != null)
            _logger.LogInformation("[RabbitMQ]: Channel successfully created");
            
        return channel;
    }
}