using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace MyLogger.Core
{
    [ProviderAlias("MyLogger")]
    internal class MyLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, MyLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, new MyLogger());

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
