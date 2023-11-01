using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace MyLogger.Core
{
    public static class MyLoggerExtensions
    {
        public static ILoggingBuilder UseMyLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();

            return builder;
        }
    }
}
