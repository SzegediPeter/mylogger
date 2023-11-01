using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLogger.Core;

namespace MyLogger.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.UseMyLogger())
              .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting application");

            logger.LogDebug("All done!");
        }
    }
}