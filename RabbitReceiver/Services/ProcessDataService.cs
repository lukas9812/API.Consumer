using System.Text.Json;
using RabbitReceiver1.Interfaces;
using RabbitReceiver1.Model;
using RollingFileWriter;

namespace RabbitReceiver1.Services;

public class ProcessDataService : IProcessDataService
{
    public void ProcessData(string data)
    {
        var model = JsonSerializer.Deserialize<List<Country>>(data);
        var germany = model!.FirstOrDefault();

        var fileWriter = new RollingFileWriterService();

        var dataToWrite = $"Country name: {germany.Name.Official}";
        
        fileWriter.WriteData(dataToWrite);
    }
}