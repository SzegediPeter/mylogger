using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLogger.Core;

namespace MyLogger.StreamTarget
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder UseStream(this ILoggingBuilder builder, IStreamProvider streamProvider)
        {
            builder.Services.AddSingleton<ILogTarget, StreamLogTarget>();
            builder.Services.AddSingleton(streamProvider);
            return builder;
        }
    }
}
