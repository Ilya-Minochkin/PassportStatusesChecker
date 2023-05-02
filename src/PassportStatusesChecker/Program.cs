using CheckerService;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBotApplication;

namespace PassportStatusesChecker
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            var bot = serviceProvider.GetService<ITelegramBot>();
            await bot.Run();
            while (true) ;
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var telegramToken = Constants.GetTelegramToken();
            services.AddTransient<ITelegramBotClient, TelegramBotClient>(_ => new TelegramBotClient(telegramToken));
            services.AddTransient<IPassportCheckerService, PassportCheckerService>(_ => new PassportCheckerService(Constants.MY_APPLICATION_NUMBER));
            services.AddTransient<ITelegramBot, PassportStatusesCheckerTelegramBot>();
            return services;
        }
    }
}