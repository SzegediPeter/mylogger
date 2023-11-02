using MyLogger.StreamTarget;

namespace MyLogger.ConsoleApp
{
    internal class SampleFileStreamProvider : IStreamProvider
    {
        private readonly FileStream _fileStream;

        public SampleFileStreamProvider(FileStream fileStream)
        {
            _fileStream = fileStream;
        }

        public Stream GetStream() => _fileStream;
    }
}
