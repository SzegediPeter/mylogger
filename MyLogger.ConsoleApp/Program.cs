using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLogger.ConsoleTarget;
using MyLogger.Core;

namespace MyLogger.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddMyLogger().UseConsole())
              .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogTrace("This is a Trace message");
            logger.LogDebug("This is a Debug message");
            logger.LogInformation("This is an Info message!");
            logger.LogWarning("This is a Warning message!");
            logger.LogError("This is an Error message!");
            logger.LogCritical("This is a Critical message!");
            logger.LogError(new Exception("This is my exception message"), "This is an error message");

            Console.ReadKey();

        }
    }
}