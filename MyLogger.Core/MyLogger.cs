using Microsoft.Extensions.Logging;

namespace MyLogger.Core
{
    public class MyLogger : ILogger
    {
        private readonly IEnumerable<ILogTarget> _logTargets;

        public MyLogger(IEnumerable<ILogTarget> logTargets)
        {
            _logTargets = logTargets;
        }

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

            var message = Format(state, logLevel, exception);

            if (_logTargets.Any())
            {
                foreach (var logTarget in _logTargets)
                {
                    logTarget.Log(logLevel, message);
                }
            }
            else
            {
                // fallback if no target registered
                lock (Console.Out)
                {
                    Console.WriteLine(message);
                }
            }

        }

        private static string Format<TState>(TState state, LogLevel logLevel, Exception exception) =>
            $"#{DateTime.UtcNow:O}[{logLevel}]#{state}";
    }
}