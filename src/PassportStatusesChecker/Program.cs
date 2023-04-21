using CheckerService;

namespace PassportStatusesChecker
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ConsularClient("2000831022023032700024570");
            await client.CheckUpdates();
        }
    }
}