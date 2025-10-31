using markdown_notes_app.Core.Interfaces.Common;
using NLog;

namespace markdown_notes_app.Infrastructure.Logging
{
    public class LoggerManager<T>: ILoggerManager<T> where T : class
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        public void LogInfo(string message)
        {
            Logger.Info(message);
        }

        public void LogWarn(string message)
        {
            Logger.Warn(message);
        }

        public void LogError(string message)
        {
            Logger.Error(message);
        }
    }
}
