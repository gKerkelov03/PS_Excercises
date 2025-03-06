using Microsoft.Extensions.Logging;
using WelcomeExtended.Helpers;

namespace WelcomeExtended.Others;

public delegate void ActionOnError(string errorMessage);

public class Delegate
{
    public static readonly ILogger logger = LoggerHelper.GetLogger("Hello");

    public static void Log(string error)
    {
        logger.LogError(error); 
    }

    public static void Log2(string error)
    {
        Console.WriteLine("- DELEGATES -");
        logger.Log(LogLevel.Error, error);
        Console.WriteLine("- DELEGATES -");
    }
}