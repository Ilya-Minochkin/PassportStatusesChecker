using Serilog;

namespace CheckerService.Logger
{
    public class ConsoleLogger : Abstractions.ILogger
    {
        private readonly Serilog.Core.Logger log;

        public ConsoleLogger()
        {
            log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void Error(string message) => log.Error(message);

        public void Information(string message) => log.Information(message);
    }
}
