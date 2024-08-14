using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitSender.Interfaces;
using RabbitSender.Services;

namespace RabbitSender;

public static class ServiceCollectionExtensions
{
    public static void AddMyServices(this IServiceCollection services)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IPublisherService, PublisherService>();
        services.AddSingleton<IApiCallerService, ApiCallerService>();
        services.AddHostedService<HostedService>();
        services.AddLogging();
    }

    public static void AddLogging(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
    }
}