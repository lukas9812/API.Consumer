using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using RabbitSender.Interfaces;

namespace RabbitSender.Services;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class HostedService : BackgroundService
{
    private readonly IPublisherService _publisherService;

    public HostedService(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _publisherService.PublishCountryData();
        return Task.CompletedTask;
    }
}