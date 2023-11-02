using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLogger.ConsoleTarget;
using MyLogger.Core;
using MyLogger.FileTarget;

namespace MyLogger.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(b => 
                    b.AddMyLogger()
                    .UseConsole()
                    .UseFile()
                    .SetMinimumLevel(LogLevel.Debug))
              .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            Parallel.For(0, 10, (i, state) =>
            {
                logger.LogTrace("This is a Trace message {counter}", i);
                logger.LogDebug("This is a Debug message {counter}", i);
                logger.LogInformation("This is an Info message {counter}", i);
                logger.LogWarning("This is a Warning message {stcounterate}", i);
                logger.LogError("This is an Error message {counter}", i);
                logger.LogCritical("This is a Critical message {counter}", i);
                logger.LogError(new Exception("This is my exception message"), "This is an error message {counter}", i);
            });



            Console.ReadKey();

        }
    }
}