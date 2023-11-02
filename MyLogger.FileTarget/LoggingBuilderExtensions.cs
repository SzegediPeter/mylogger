using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLogger.Core;

namespace MyLogger.FileTarget
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder UseFile(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILogTarget, FileLogTarget>();
            builder.Services.AddSingleton<IFileSystem, FileSystem>();
            return builder;
        }
    }
}
