using CheckerService.Logger;
using CheckerService.Logger.Abstractions;
using CheckerService.Merge;
using CheckerService.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace CheckerService
{
    public interface IPassportCheckerService
    {
        Task<ReadinessResponce> GetStatus();
        Task SaveToFile(string filePath, ReadinessResponce responce);
        Task<MergeResult> MergeWithFile(string filePath, ReadinessResponce responce);
    }
    public class PassportCheckerService : IPassportCheckerService
    {
        private readonly ConsularClient client;
        private readonly ILogger log;

        public PassportCheckerService(string applicationNumber)
        {
            client = new ConsularClient(applicationNumber);
            log = new ConsoleLogger();
        }

        public async Task<ReadinessResponce> GetStatus()
        {
            try
            {
                var responce = await client.CheckUpdates();
                if (responce == null)
                    throw new Exception("Responce exception");

                log.Information($"Responce getted - {responce}");
                return responce;
            }
            catch (Exception ex)
            {
                log.Error("During request " + ex.Message);
                throw;
            }
        }

        public async Task<MergeResult> MergeWithFile(string filePath, ReadinessResponce responce)
        {
            var responceFromFile = new ReadinessResponce();
            if (File.Exists(filePath))
            {
                var text = await File.ReadAllTextAsync(filePath);
                responceFromFile = JsonSerializer.Deserialize<ReadinessResponce>(text);
                if (responceFromFile == null)
                    throw new Exception("Can't deserialize responce from file");
            }

            return ResponceMerger.Merge(responceFromFile, responce);
        }

        public async Task SaveToFile(string filePath, ReadinessResponce responce)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(responce, options);
                log.Information("New file saved");
                await File.WriteAllTextAsync(filePath, jsonString, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                log.Error($"Error: {ex.Message}");  
            }
        }
    }
}
