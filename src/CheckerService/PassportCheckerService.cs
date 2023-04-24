using CheckerService.Merge;
using CheckerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace CheckerService
{
    public interface IPassportCheckerService
    {
        Task<ReadinessResponce> GetStatus();
        Task SaveToFile(string filePath, ReadinessResponce responce);
        Task<List<Difference>> MergeWithFile(string filePath, ReadinessResponce responce);
    }
    public class PassportCheckerService : IPassportCheckerService
    {
        private readonly ConsularClient client;

        public PassportCheckerService(string applicationNumber)
        {
            client = new ConsularClient(applicationNumber);
        }

        public async Task<ReadinessResponce> GetStatus()
        {
            var responce = await client.CheckUpdates();
            if (responce == null)
                throw new Exception("Responce exception");

            return responce;
        }

        public async Task<List<Difference>> MergeWithFile(string filePath, ReadinessResponce responce)
        {
            var text = await File.ReadAllTextAsync(filePath);
            var responceFromFile = JsonSerializer.Deserialize<ReadinessResponce>(text);

            if (responceFromFile == null)
                throw new Exception("Can't deserialize responce from file");

            return ResponceMerger.Merge(responceFromFile, responce);
        }

        public async Task SaveToFile(string filePath, ReadinessResponce responce)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(responce, options);
            await File.WriteAllTextAsync(filePath, jsonString, Encoding.UTF8);    
        }
    }
}
