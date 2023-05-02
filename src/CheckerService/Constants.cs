namespace CheckerService
{
    public class Constants
    {
        public const string URL_MIDPASS = "https://info.midpass.ru/api/request/";
        public const long MY_CHAT_ID = 838343374;
        public const string MY_APPLICATION_NUMBER = "2000831022023032700024570";
        public static string? GetTelegramToken()
        {
            return Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
        }
    }
}
