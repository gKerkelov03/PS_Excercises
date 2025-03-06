using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Logging;

namespace WelcomeExtended.Loggers;

public class HashLogger : ILogger
{
    private readonly ConcurrentDictionary<int, string> _logMessages;
    private readonly string _name;

    public HashLogger(string name)
    {
        _logMessages = new ConcurrentDictionary<int, string>();
        _name = name;
    }
    public IDisposable BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }
    

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public void Log<TState>(
        LogLevel logLevel, EventId eventId,
        TState state, Exception? exception,
        Func<TState, Exception, string> formatter)
    {
        var msessage = formatter(state, exception);

        switch (logLevel)
        {
            case LogLevel.Critical:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                break;
            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }

        Console.WriteLine("- LOGGER -");
        var messageTeßeLogged = new StringBuilder();
        messageTeßeLogged.Append($"|{logLevel}]");
        messageTeßeLogged.AppendFormat($"|{0}]", _name);
        Console.WriteLine(messageTeßeLogged);
        Console.WriteLine($"{formatter(state, exception)}");
        Console.WriteLine("- LOGGER -");
        Console.ResetColor();
        
        _logMessages[eventId.Id] = msessage;
    }
}