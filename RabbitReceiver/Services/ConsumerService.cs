using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitReceiver1.Interfaces;
using RabbitReceiver1.Model;

namespace RabbitReceiver1.Services;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class ConsumerService : IConsumerService
{
    private readonly AppSettings _settings;
    private readonly IModel _model;
    private readonly ILogger<ConsumerService> _logger;
    private readonly IProcessDataService _processDataService;

    public ConsumerService(
        IRabbitMqService rabbitMqService,
        IOptions<AppSettings> options,
        ILogger<ConsumerService> logger, IProcessDataService processDataService)
    {
        _settings = options.Value;
        _logger = logger;
        _processDataService = processDataService;
        var connection = rabbitMqService.CreateChannel();
        _model = connection!.CreateModel();
    }

    public void Consume()
    {
        var rabbitSettings = _settings.RabbitMq;
        if (rabbitSettings is null)
            throw new ApplicationException("Application settings are not initialized!");
            
        _model.ExchangeDeclare(rabbitSettings.ExchangeName, ExchangeType.Direct);
        _model.QueueDeclare(rabbitSettings.QueueName, false, false, false, null);
        _model.QueueBind(rabbitSettings.QueueName, rabbitSettings.ExchangeName, rabbitSettings.RoutingKey, null);
        _model.BasicQos(0, 1, false);

        var consumer = new EventingBasicConsumer(_model);
        consumer.Received += (sender, args) =>
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            _processDataService.ProcessData(message);

            _model.BasicAck(args.DeliveryTag, false);
        };

        var consumerTag = _model.BasicConsume(rabbitSettings.QueueName, false, consumer);
    }
}