using Microsoft.Extensions.Logging;
using NodaTime;

namespace MyLogger.Core
{
    public class MyLogger : ILogger
    {
        private readonly IEnumerable<ILogTarget> _logTargets;
        private readonly IClock _clock;

        public MyLogger(IEnumerable<ILogTarget> logTargets, IClock clock)
        {
            _logTargets = logTargets;
            _clock = clock;
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
                Parallel.ForEach(_logTargets, target =>
                {
                    target.Log(logLevel, message);
                });
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

        private string Format<TState>(TState state, LogLevel logLevel, Exception exception) =>
            $"#{_clock.GetCurrentInstant().ToDateTimeUtc():O}[{logLevel}]#{state}";
    }
}