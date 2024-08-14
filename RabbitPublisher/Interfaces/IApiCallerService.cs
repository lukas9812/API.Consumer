namespace RabbitSender.Interfaces;

public interface IApiCallerService
{
    Task<byte[]?> GetCountryJsonInfo();
}