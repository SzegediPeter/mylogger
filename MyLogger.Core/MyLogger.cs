using Microsoft.Extensions.Logging;

namespace MyLogger.Core
{
    public class MyLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return default!; // TODO
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true; // TODO
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            Console.WriteLine(Format(state, logLevel, exception));
        }

        private static string Format<TState>(TState state, LogLevel logLevel, Exception exception) =>
            $"#{DateTime.UtcNow:O}[{logLevel}]#{state}";
    }
}