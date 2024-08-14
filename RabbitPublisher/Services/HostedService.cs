using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using RabbitSender.Interfaces;

namespace RabbitSender.Services;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class HostedService : IHostedService
{
    private readonly IPublisherService _publisherService;

    public HostedService(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _publisherService.PublishCountryData();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}