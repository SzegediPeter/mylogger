using Microsoft.Extensions.Logging;
using Moq;
using NodaTime;
using NodaTime.Testing;

namespace MyLogger.Core.Unit.Tests
{
    public class MyLoggerTests
    {
        private readonly MyLogger _sut;

        private readonly Mock<ILogTarget> _logTargetMock1 = new();
        private readonly Mock<ILogTarget> _logTargetMock2 = new();

        private readonly IClock _clock;

        private readonly DateTime _now = new(2023, 10, 24, 14, 45, 56, DateTimeKind.Utc);

        public MyLoggerTests()
        {
            _clock = new FakeClock(Instant.FromDateTimeUtc(_now));
            _sut = new MyLogger(new[] { _logTargetMock1.Object, _logTargetMock2.Object }, _clock);
        }

        [Fact]
        public void Log_WhenTargetsAreProvided_ShouldCallAllTargets()
        {
            // Arrange
            var logLevel = LogLevel.Debug;
            var formattedMessage = "#2023-10-24T14:45:56.0000000Z[Debug]#myState";

            // Act
            _sut.Log(logLevel, new(), "myState", null, (_, _) => formattedMessage);

            // Assert
            _logTargetMock1.Verify(lt => lt.Log(logLevel, formattedMessage), Times.Once);
            _logTargetMock2.Verify(lt => lt.Log(logLevel, formattedMessage), Times.Once);
        }
    }
}