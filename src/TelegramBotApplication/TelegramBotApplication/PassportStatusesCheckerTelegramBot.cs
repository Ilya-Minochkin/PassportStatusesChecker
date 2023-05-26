using CheckerService;
using CheckerService.Merge;
using CheckerService.Services;
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
        private readonly IPassportCheckerService passportCheckerService;
        private readonly IChatService chatService;
        private readonly string pathToLastStatusFile;
        private Dictionary<long, ApplicationTimer> chatsInfo = new();
        private Timer? timer;

        public PassportStatusesCheckerTelegramBot(ITelegramBotClient client
            , IPassportCheckerService passportCheckerService
            , IChatService chatService)
        {
            bot = client;
            this.passportCheckerService = passportCheckerService;
            this.chatService = chatService;
            pathToLastStatusFile = GetPathToFile();
        }

        public async Task Run()
        {
            bot.StartReceiving(
                HandleMessage,
                HandleError
                );

            await InitializeTimers();
        }

        private async Task Check(long chatId, string applicationNumber)
        {
            try
            {
                var currentResponse = await passportCheckerService.GetStatus(applicationNumber);
                var mergeResult = await passportCheckerService.MergeWithDatabase(currentResponse);
                if (!mergeResult.ResultEquals)
                { 
                    await SendActualStatusMessage(chatId, mergeResult.ToString());
                    await passportCheckerService.SaveToDatabase(currentResponse);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
        }

        private async Task SendActualStatusMessage(long chatId, string message)
        {
            await bot.SendTextMessageAsync(new ChatId(chatId), message, Telegram.Bot.Types.Enums.ParseMode.Markdown);
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
        private async Task InitializeTimers()
        {
            var chats = await chatService.GetAll();
            foreach (var chat in chats)
            {
                foreach (var application in chat.Applications)
                {
                    chatsInfo.TryAdd(chat.ChatId, new ApplicationTimer()
                    {
                        ApplicationNumber = application.Number,
                        Timer = new Timer(async _ =>
                        {
                            await Task.Run(() => Check(chat.ChatId, application.Number));
                        }, null, TimeSpan.Zero, TimeSpan.FromHours(12))
                    });
                }
            }
        }
        private class ApplicationTimer
        {
            public string ApplicationNumber {get; set; }
            public Timer Timer {get; set; }
        }
    }
}