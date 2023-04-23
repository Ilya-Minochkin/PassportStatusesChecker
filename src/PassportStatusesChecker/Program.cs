using CheckerService;
using System.Reflection;

namespace PassportStatusesChecker
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ConsularClient("2000831022023032700024570");
            IPassportCheckerService service = new PassportCheckerService(client);
            var responce = await service.GetStatus();
            var fileName = Directory.GetCurrentDirectory() + "\\LastResponce.json";
            await service.SaveToFile(fileName, responce);
        }
    }
}