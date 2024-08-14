namespace RabbitSender.Model;

public class RabbitMq
{
    public string RabbitUri { get; set; } = string.Empty;
    
    public string ExchangeName { get; set; } = string.Empty;
    
    public string RoutingKey { get; set; } = string.Empty;
    
    public string QueueName { get; set; } = string.Empty;
    
}