
using TelegramBotApplication;

namespace PassportStatusesChecker
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await new PassportStatusesCheckerTelegramBot().Run();

            while (true) ;
        }
    }
}