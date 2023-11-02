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