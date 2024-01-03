using AOMTEST.Logger.Interfaces;

namespace AOMTEST.Logger
{
    public class LoggerSecurityService : ILoggerSecurityService
    {
        public async Task LogAsync(string message)
        {
            await Task.FromResult(message);
        }
    }
}
