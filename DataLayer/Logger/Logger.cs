using DataLayer.Database;
using DataLayer.Model;

namespace DataLayer.Logger;

public class Logger
{
    private readonly DatabaseContext _context;

    public Logger(DatabaseContext context)
    {
        _context = context;
    }

    public void LogAction(string action, string details, string username)
    {
        var logEntry = new LogEntry
        {
            Action = action,
            Details = details,
            Timestamp = DateTime.Now,
            Username = username
        };

        _context.LogEntries.Add(logEntry);
        _context.SaveChanges();
    }
} 