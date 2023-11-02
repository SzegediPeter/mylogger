using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MyLogger.Core
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddMyLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();

            return builder;
        }
    }
}
