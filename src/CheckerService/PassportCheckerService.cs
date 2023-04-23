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
        Task MergeWithFile(string filePath, ReadinessResponce responce);
    }
    public class PassportCheckerService : IPassportCheckerService
    {
        private readonly ConsularClient client;

        public PassportCheckerService(ConsularClient client)
        {
            this.client = client;
        }

        public async Task<ReadinessResponce> GetStatus()
        {
            var responce = await client.CheckUpdates();
            if (responce == null)
                throw new Exception("Responce exception");

            return responce;
        }

        public async Task MergeWithFile(string filePath, ReadinessResponce responce)
        {
            throw new NotImplementedException();
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
