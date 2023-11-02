using Microsoft.Extensions.Logging;

namespace MyLogger.Core
{
    public interface ILogTarget
    {
        Task Log(LogLevel logLevel, string message);
    }
}
