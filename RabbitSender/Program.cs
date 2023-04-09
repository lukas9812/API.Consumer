using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sender App";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

var exchangeName = "DemoExchange";
var routingKey = "demo-routing-key";
var queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false,false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);

for (int i = 0; i < 60; i++)
{
    Console.WriteLine("SENDING MESSAGE NUMBER {0}", i);
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    channel.BasicPublish(exchangeName, routingKey,null, messageBodyBytes);
    Thread.Sleep(1000);
}


channel.Close();
connection.Close();