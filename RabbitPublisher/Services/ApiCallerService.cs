using System.Text;

namespace RabbitSender.Services;

public class ApiCallerService
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

            // var countryList = JsonConvert.DeserializeObject<List<Country>>(responseBody);
            // if (countryList is null)
            //     throw new Exception();
            // var country = countryList.FirstOrDefault();
            //
            // var officialName = country!.Name.Official;
            // var currency = country!.Currencies
            //     .Select(x => x.Value.Name)
            //     .FirstOrDefault();

            // Console.WriteLine($"Official name of {Country} is: {officialName} and currency is: {currency}");
            
            return Encoding.UTF8.GetBytes(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
            return null;
        }
    }
}