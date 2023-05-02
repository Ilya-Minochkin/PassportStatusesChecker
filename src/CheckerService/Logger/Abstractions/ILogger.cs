namespace CheckerService.Logger.Abstractions
{
    public interface ILogger
    {
        void Information(string message);
        void Error(string message);
    }
}
