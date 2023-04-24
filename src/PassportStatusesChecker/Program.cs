using CheckerService;
using CheckerService.Merge;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace PassportStatusesChecker
{
    public class Program
    {
        static ITelegramBotClient bot;

        static async Task Main(string[] args)
        {
            var telegramToken = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
            bot = new TelegramBotClient(telegramToken);
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync
            );
            IPassportCheckerService service = new PassportCheckerService("2000831022023032700024570");
            var responce = await service.GetStatus();
            var fileName = Directory.GetCurrentDirectory() + "\\LastResponce.json";
            await service.SaveToFile(fileName, responce);
            var mergeResult = await service.MergeWithFile(fileName, responce);
            if (mergeResult.Count > 0 )
            {
                var diffirences = GetMergeMessage(mergeResult);

            }
            while(true)
            {

            }
        }
        private static string GetMergeMessage(List<Difference> differences)
        {
            var sb = new StringBuilder();
            foreach (var difference in differences)
                sb.AppendLine($"Было: {difference.LeftValue} \nСтало: {difference.RightValue}");
            return sb.ToString();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Я живой!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}