using Microsoft.Extensions.Hosting;
using RabbitSender;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMyServices();
builder.Logging.AddLogging();

using var host = builder.Build();

await host.RunAsync();

