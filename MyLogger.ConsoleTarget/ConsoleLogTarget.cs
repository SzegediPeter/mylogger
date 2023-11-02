using Microsoft.Extensions.Logging;
using MyLogger.Core;

namespace MyLogger.ConsoleTarget
{
    public class ConsoleLogTarget : ILogTarget
    {
        private const int MaxLength = 1000;

        public void Log(LogLevel logLevel, string message)
        {
            if (message.Length > MaxLength)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(message),
                    $"Should not be longer than {MaxLength}"); // This is a very bad idea, log should never throw an exception but this is the requirement.
            }

            var color = logLevel switch
            {
                <= LogLevel.Debug => ConsoleColor.Gray,
                <= LogLevel.Information => ConsoleColor.Green,
                _ => ConsoleColor.Red
            };

            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}