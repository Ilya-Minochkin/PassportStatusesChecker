using CheckerService;
using CheckerService.Services;
using Database;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using CheckerService.Logger;
using CheckerService.Logger.Abstractions;
using CheckerService.Models;
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
            var chatService = serviceProvider.GetService<IChatService>();
            var newChat = new Chat()
            {
                ChatId = 838343374
            };
            //await chatService.Add(newChat);
            await bot.Run();
            while (true) ;
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var telegramToken = Constants.GetTelegramToken();
            //services.AddDbContext<PgDbContext>();
            services.AddDbContext<PgDbContext>(options =>
                options.UseNpgsql(ConfigurationManager.AppSettings.Get("ConnectionString")));
            services.AddTransient<ITelegramBotClient, TelegramBotClient>(_ => new TelegramBotClient(telegramToken));
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IPassportCheckerService, PassportCheckerService>();
            services.AddTransient<ITelegramBot, PassportStatusesCheckerTelegramBot>();
            services.AddTransient<IChatsRepository, ChatsRepository>();
            services.AddTransient<IReadinessResponsesRepository, ReadinessResponsesRepository>();
            services.AddTransient<IPublicStatusesRepository, PublicStatusesRepository>();
            services.AddTransient<IInternalStatusesRepository, InternalStatusesRepository>();
            return services;
        }
    }
}