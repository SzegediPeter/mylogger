using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using NodaTime;

namespace MyLogger.Core
{
    internal class MyLoggerProvider : ILoggerProvider
    {
        private readonly IEnumerable<ILogTarget> _logTargets;
        private readonly IClock _clock;
        private readonly ConcurrentDictionary<string, MyLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public MyLoggerProvider(IEnumerable<ILogTarget> logTargets, IClock clock)
        {
            _logTargets = logTargets;
            _clock = clock;
        }

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, new MyLogger(_logTargets, _clock));

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
