using System.Text;
using RabbitSender.Interfaces;

namespace RabbitSender.Services;

public class ApiCallerService : IApiCallerService
{
    private const string Country = "deutschland";

    private const string ApiCallString = $"https://restcountries.com/v3.1/name/{Country}";

    public async Task<byte[]?> GetCountryJsonInfo()
    {
        using var client = new HttpClient();
        try
        {
            // Send GET request to the API
            var response = await client.GetAsync(ApiCallString);
            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return Encoding.UTF8.GetBytes(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
            return null;
        }
    }
}