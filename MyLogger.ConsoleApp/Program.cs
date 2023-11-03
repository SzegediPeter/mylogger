using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLogger.ConsoleTarget;
using MyLogger.Core;
using MyLogger.FileTarget;
using MyLogger.StreamTarget;

namespace MyLogger.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tempFile  = Path.GetTempFileName();
            Console.WriteLine(tempFile);

            using FileStream fs =  new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Write);
            SampleFileStreamProvider sampleFileStreamProvider = new(fs);
            
                var serviceProvider = new ServiceCollection()
                .AddLogging(b =>
                    b.AddMyLogger()
                    .UseConsole()
                    .UseFile()
                    .UseStream(sampleFileStreamProvider)
                    .SetMinimumLevel(LogLevel.Trace))
              .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting parallel loop {DateTime:o}", DateTime.Now);

            Parallel.For(0, 10, (i, _) =>
            {
                logger.LogTrace("This is a Trace message {counter}", i);
                logger.LogDebug("This is a Debug message {counter}", i);
                logger.LogInformation("This is an Info message {counter}", i);
                logger.LogWarning("This is a Warning message {counter}", i);
                logger.LogError("This is an Error message {counter}", i);
                logger.LogCritical("This is a Critical message {counter}", i);
                logger.LogError(new Exception("This is my exception message"), "This is an error message {counter}", i);
            });

            logger.LogTrace("This is a Trace message {counter}", -1);
            logger.LogDebug("This is a Debug message {counter}", -1);
            logger.LogInformation("This is an Info message {counter}", -1);
            logger.LogWarning("This is a Warning message {counter}", -1);
            logger.LogError("This is an Error message {counter}", -1);
            logger.LogCritical("This is a Critical message {counter}", -1);
            logger.LogError(new Exception("This is my exception message"), "This is an error message {counter}", -1);

            Console.WriteLine("Program DONE");
            Console.ReadKey();
        }
    }
}