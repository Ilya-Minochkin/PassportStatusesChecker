using CheckerService;
using CheckerService.Merge;
using System.Reflection;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotApplication.Exceptions;

namespace TelegramBotApplication
{
    public interface ITelegramBot
    {
        Task Run();
    }
    public class PassportStatusesCheckerTelegramBot : ITelegramBot
    {
        private readonly ITelegramBotClient bot;
        private readonly IPassportCheckerService service;
        private readonly string pathToLastStatusFile;
        private Timer? timer;

        public PassportStatusesCheckerTelegramBot(ITelegramBotClient client, IPassportCheckerService passportCheckerService)
        {
            bot = client;
            service = passportCheckerService;
            pathToLastStatusFile = GetPathToFile();
        }

        public async Task Run()
        {
            bot.StartReceiving(
                HandleMessage,
                HandleError
                );

            timer = new Timer(async (_) =>
                    await Task.Run(() => Check())
                , null, TimeSpan.Zero, TimeSpan.FromDays(1));
        }

        private async Task Check()
        {
            try
            {
                var currentResponce = await service.GetStatus();
                var differences = await service.MergeWithFile(pathToLastStatusFile, currentResponce);
                if (differences.Count > 0)
                {
                    var diffMessage = BuildDifferenceMessage(differences);
                    await SendActualStatusMessage(diffMessage);
                    await service.SaveToFile(pathToLastStatusFile, currentResponce);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
        }

        private async Task SendActualStatusMessage(string message)
        {
            await bot.SendTextMessageAsync(new ChatId(Constants.MY_CHAT_ID), message);
        }

        private static string BuildDifferenceMessage(IEnumerable<Difference> differences)
        {
            var sb = new StringBuilder();

            foreach (var difference in differences)
                sb.AppendLine($"Было: {difference.LeftValue ?? "null"}\n Стало: {difference.RightValue}");

            return sb.ToString();
        }

        private async Task HandleMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null)
                await botClient.SendTextMessageAsync(update.Message.Chat.Id
                    , "Бот не принимает команд на текущий момент."
                    , cancellationToken: cancellationToken);
        }

        private async Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
        }

        private static string GetPathToFile()
        {
            var pathToAssembly = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var filesDirectoryName = "StatusFiles";

            if (pathToAssembly == null)
                throw new Exception("Cant get path to assembly");

            var finalPath = Path.Combine(pathToAssembly, filesDirectoryName);

            if (!Directory.Exists(finalPath))
                Directory.CreateDirectory(finalPath);

            return Path.Combine(finalPath, "LastStatus.json");
        }
    }
}