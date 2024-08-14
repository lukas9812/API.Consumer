using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitSender.Interfaces;
using RabbitSender.Model;
using RabbitSender.Services;

namespace RabbitSender;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, HostApplicationBuilder builder)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IPublisherService, PublisherService>();
        services.AddSingleton<IApiCallerService, ApiCallerService>();
        services.AddHostedService<HostedService>();
        services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

        services.AddLogging();
    }

    public static void AddLogging(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
    }
}