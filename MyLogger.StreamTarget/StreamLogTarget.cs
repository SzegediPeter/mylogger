using Microsoft.Extensions.Logging;
using MyLogger.Core;

namespace MyLogger.StreamTarget
{
    public class StreamLogTarget : ILogTarget
    {
        private readonly Stream _synchronizedStream;

        public StreamLogTarget(IStreamProvider streamProvider)
        {
            _synchronizedStream = Stream.Synchronized(streamProvider.GetStream());
        }

        public async Task Log(LogLevel logLevel, string message)
        {
            StreamWriter sw = new(_synchronizedStream);
            sw.AutoFlush = true;
            await sw.WriteLineAsync(message);
        }
    }
}