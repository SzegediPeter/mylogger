using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace MyLogger.Core
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddMyLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
            builder.Services.AddTransient<IClock>(_ => SystemClock.Instance);
            return builder;
        }
    }
}
