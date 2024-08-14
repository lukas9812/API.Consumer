using System.Diagnostics.CodeAnalysis;
using RabbitMQ.Client;
using RabbitSender.Interfaces;

namespace RabbitSender.Services;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class PublisherService : IPublisherService, IDisposable
{
    private readonly IApiCallerService _apiCallerService;
    
    private readonly IModel _model;
    private readonly IConnection _connection;

    public PublisherService(IApiCallerService apiCallerService, IRabbitMqService rabbitMqService)
    {
        _apiCallerService = apiCallerService;
        _connection = rabbitMqService.CreateChannel();
        _model = _connection.CreateModel();
    }

    public async Task PublishCountryData()
    {
        var exchangeName = "DemoExchange";
        var routingKey = "demo-routing-key";
        var queueName = "DemoQueue";

        _model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        _model.QueueDeclare(queueName, false, false,false, null);
        _model.QueueBind(queueName, exchangeName, routingKey, null);
        
        var countryJsonInfo = await _apiCallerService.GetCountryJsonInfo();
        
        _model.BasicPublish(exchangeName, routingKey, null, countryJsonInfo);
        Thread.Sleep(1000);
    }

    public void Dispose()
    {
        // _model.Dispose();
        // _connection.Dispose();
    }
}