using Microsoft.Extensions.Logging;

namespace WelcomeExtended.Loggers;

public class LoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new HashLogger(categoryName);
    }
}