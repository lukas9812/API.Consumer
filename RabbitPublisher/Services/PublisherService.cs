using RabbitMQ.Client;

namespace RabbitSender.Services;

public class PublisherService : IPublisher
{
    private ApiCallerService? ApiCaller { get; set; }
    
    public async Task PublishCountryData()
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672"),
            ClientProvidedName = "Rabbit Publisher App"
        };

        IConnection connection = factory.CreateConnection();
        IModel channel = connection.CreateModel();

        var exchangeName = "DemoExchange";
        var routingKey = "demo-routing-key";
        var queueName = "DemoQueue";

        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false,false, null);
        channel.QueueBind(queueName, exchangeName, routingKey, null);

        ApiCaller ??= new ApiCallerService();
        
        var countryJsonInfo = await ApiCaller!.GetCountryJsonInfo();
        
        channel.BasicPublish(exchangeName, routingKey, null, countryJsonInfo);
        Thread.Sleep(1000);

        channel.Close();
        connection.Close();
    }
}