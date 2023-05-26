using CheckerService.Logger;
using CheckerService.Logger.Abstractions;
using CheckerService.Merge;
using CheckerService.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using CheckerService.Mapper;
using Database.Repositories;
using System.Net;

namespace CheckerService.Services
{
    public interface IPassportCheckerService
    {
        Task<ReadinessResponse> GetStatus(string applicationNumber);
        Task SaveToFile(string filePath, ReadinessResponse response);
        Task<MergeResult> MergeWithFile(string filePath, ReadinessResponse response);
        Task<MergeResult> MergeWithDatabase(ReadinessResponse response);
        Task SaveToDatabase(ReadinessResponse response);
    }
    public class PassportCheckerService : IPassportCheckerService
    {
        private readonly ConsularClient client;
        private readonly ILogger log;
        private readonly IReadinessResponsesRepository responsesRepository;
        private readonly IChatsRepository chatsRepository;

        public PassportCheckerService(ILogger log, IReadinessResponsesRepository responsesRepository
            , IChatsRepository chatsRepository)
        {
            client = new();
            this.log = log;
            this.responsesRepository = responsesRepository;
            this.chatsRepository = chatsRepository;
        }

        public async Task<ReadinessResponse> GetStatus(string applicationNumber)
        {
            try
            {
                var response = await client.CheckUpdates(applicationNumber);
                if (response == null)
                    throw new("response exception");

                log.Information($"response get - {response} for applicationNumber={applicationNumber}");
                return response;
            }
            catch (Exception ex)
            {
                log.Error("During request " + ex.Message);
                throw;
            }
        }

        public async Task<MergeResult> MergeWithFile(string filePath, ReadinessResponse response)
        {
            var responseFromFile = new ReadinessResponse();
            if (File.Exists(filePath))
            {
                var text = await File.ReadAllTextAsync(filePath);
                responseFromFile = JsonSerializer.Deserialize<ReadinessResponse>(text);
                if (responseFromFile == null)
                    throw new("Can't deserialize response from file");
            }

            return ResponceMerger.Merge(responseFromFile, response);
        }

        public async Task<MergeResult> MergeWithDatabase(ReadinessResponse response)
        {
            var dbStoredResponse = await responsesRepository.FindByUidAndChatId(response.Uid, response.Chat.ChatId);
            ReadinessResponse storedResponse = new();

            if (dbStoredResponse != null)
                storedResponse =
                    SimpleObjectMapper.GetMappedObject<Database.Entities.ReadinessResponse, ReadinessResponse>(
                        dbStoredResponse);

            return ResponceMerger.Merge(storedResponse, response);
        }

        public async Task SaveToDatabase(ReadinessResponse response)
        {
            var responseExist = await responsesRepository.FindByUidAndChatId(response.Uid, response.Chat.ChatId) is not null;
            var dbResponse = SimpleObjectMapper.GetMappedObject<ReadinessResponse, Database.Entities.ReadinessResponse>(response);

            if (responseExist)
            {
                await responsesRepository.Update(dbResponse);
            }
            else
            {
                await responsesRepository.Save(dbResponse);
            }
        }

        public async Task SaveToFile(string filePath, ReadinessResponse response)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(response, options);
                log.Information("New file saved");
                await File.WriteAllTextAsync(filePath, jsonString, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                log.Error($"Error: {ex.Message}");
            }
        }
    }
}
