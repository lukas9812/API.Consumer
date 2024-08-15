using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitSender.Interfaces;
using RabbitSender.Model;

namespace RabbitSender.Services;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class PublisherService : IPublisherService, IDisposable
{
    private readonly IApiCallerService _apiCallerService;
    private readonly AppSettings _settings;
    private readonly IModel _model;
    private readonly ILogger<PublisherService> _logger;

    public PublisherService(
        IApiCallerService apiCallerService,
        IRabbitMqService rabbitMqService,
        IOptions<AppSettings> options, 
        ILogger<PublisherService> logger)
    {
        _settings = options.Value;
        _apiCallerService = apiCallerService;
        _logger = logger;
        var connection = rabbitMqService.CreateChannel();
        _model = connection!.CreateModel();
    }

    public async Task PublishCountryData()
    {
        var rabbitSettings = _settings.RabbitMq;
        if (rabbitSettings is null)
            throw new ApplicationException("Application settings are not initialized!");

        _model.ExchangeDeclare(rabbitSettings.ExchangeName, ExchangeType.Direct);
        _model.QueueDeclare(rabbitSettings.QueueName, false, false, false, null);
        _model.QueueBind(rabbitSettings.QueueName, rabbitSettings.ExchangeName, rabbitSettings.RoutingKey, null);
        
        _logger.LogInformation("Getting country data from API.");
        var countryJsonInfo = await _apiCallerService.GetCountryJsonInfo();
        
        _model.BasicPublish(rabbitSettings.ExchangeName, rabbitSettings.RoutingKey, null, countryJsonInfo);
        Thread.Sleep(1000);
    }

    public void Dispose()
    {
        // _model.Dispose();
        // _connection.Dispose();
    }
}