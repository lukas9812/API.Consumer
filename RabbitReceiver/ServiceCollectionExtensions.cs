using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitReceiver1.Interfaces;
using RabbitReceiver1.Model;
using RabbitReceiver1.Services;

namespace RabbitReceiver1;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, HostApplicationBuilder builder)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IConsumerService, ConsumerService>();
        services.AddSingleton<IProcessDataService, ProcessDataService>();
        services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

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