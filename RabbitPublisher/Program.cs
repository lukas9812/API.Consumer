using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitSender;
using RabbitSender.Services;

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddSingleton<IPublisher, PublisherService>()
    .BuildServiceProvider();

//configure console logging
serviceProvider.GetService<ILoggerFactory>();

var logger = serviceProvider.GetService<ILoggerFactory>()!
    .CreateLogger<Program>();

logger.LogDebug("Starting application");

