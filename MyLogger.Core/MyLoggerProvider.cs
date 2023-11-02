using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace MyLogger.Core
{
    [ProviderAlias("MyLogger")]
    internal class MyLoggerProvider : ILoggerProvider
    {
        private readonly IEnumerable<ILogTarget> _logTargets;
        private readonly ConcurrentDictionary<string, MyLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public MyLoggerProvider(IEnumerable<ILogTarget> logTargets)
        {
            _logTargets = logTargets;
        }

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, new MyLogger(_logTargets));

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
