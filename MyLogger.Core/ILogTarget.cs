using Microsoft.Extensions.Logging;

namespace MyLogger.Core
{
    public interface ILogTarget
    {
        void Log(LogLevel logLevel, string message);
    }
}
