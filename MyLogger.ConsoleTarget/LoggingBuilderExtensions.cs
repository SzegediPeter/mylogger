using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLogger.Core;

namespace MyLogger.ConsoleTarget
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder UseConsole(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILogTarget, ConsoleLogTarget>();

            return builder;
        }
    }
}
