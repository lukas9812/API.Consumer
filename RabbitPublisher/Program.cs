using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitSender;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.ConfigureServices(builder);
builder.Logging.AddLogging();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables();

using var host = builder.Build();

await host.RunAsync();

