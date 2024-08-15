using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using RabbitReceiver1.Interfaces;

namespace RabbitReceiver1.Services;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class HostedService : BackgroundService
{
    private readonly IConsumerService _consumerService;

    public HostedService(IConsumerService consumerService)
    {
        _consumerService = consumerService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumerService.Consume();
        return Task.CompletedTask;
    }
}