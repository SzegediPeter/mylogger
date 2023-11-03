# MyLogger logging component

## Usage

```csharp
var serviceProvider = new ServiceCollection()
    .AddLogging(b => 
        b.AddMyLogger() // Add logger to service collection
        .UseConsole() // To use console logging
        .UseFile() // To use file logging
    .BuildServiceProvider();
```

## Packages

### MyLogger.Core

Core package no need to reference it directly.

### MyLogger.ConsoleTarget

Console Target package. Reference it in case you need simple console logging.

### MyLogger.FileTarget

File Target package. Reference it in case you need file based logging.

### MyLogger.StreamTarget

Stream Target package. Reference it in case you need custom Stream based logging.