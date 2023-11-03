using Microsoft.Extensions.Logging;
using MyLogger.Core;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace MyLogger.FileTarget
{
    public class FileLogTarget : ILogTarget
    {
        private const string Pattern = @"\.(?<integer>\d+)\.";
        private const int MaxFileSize = 5 * 1024;
        private const string LogFileName = "log.txt";

        private readonly IFileSystem _fileSystem;
        private readonly string _basePath = AppContext.BaseDirectory;
        private readonly object _lock = new();

        public FileLogTarget(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Task Log(LogLevel logLevel, string message)
        {
            lock (_lock)
            {
                CheckFileSize();
                _fileSystem.File.AppendAllText(CurrentLogFilePath, $"{message}\n");
            }

            return Task.CompletedTask;
        }

        private string CurrentLogFilePath => Path.Combine(_basePath, LogFileName);

        private void CheckFileSize()
        {
            var fi = _fileSystem.FileInfo.New(CurrentLogFilePath);
            
            if (fi.Exists && fi.Length > MaxFileSize)
            {
                var logFileList = _fileSystem.Directory.GetFiles(_basePath, "log.*.txt", SearchOption.TopDirectoryOnly);

                var latestIndex = logFileList.Any() ? logFileList.Select(ExtractInteger).Max() : 0;
                var nextIndex = latestIndex + 1;
                _fileSystem.File.Move(CurrentLogFilePath, Path.Combine(_basePath, $"log.{nextIndex}.txt"));
            }
        }

        private int ExtractInteger(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return -1;
            }

            var match = Regex.Match(fileName, Pattern);

            if (match.Success)
            {
                return int.Parse(match.Groups["integer"].Value);
            }

            return -1;
        }
    }
}