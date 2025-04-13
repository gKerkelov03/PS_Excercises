using System;
using System.Threading.Tasks;
using DataLayer.Database;
using DataLayer.Model;

namespace DataLayer
{
    public class Logger
    {
        private readonly DatabaseContext _context;

        public Logger(DatabaseContext context)
        {
            _context = context;
        }

        // Synchronous methods
        public void LogAction(string level, string message, string username = "System")
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = level,
                Message = message,
                Username = username
            };

            _context.LogEntries.Add(logEntry);
            _context.SaveChanges();
        }

        public void LogInfo(string message, string username = "System")
        {
            LogAction("INFO", message, username);
        }

        public void LogWarning(string message, string username = "System")
        {
            LogAction("WARNING", message, username);
        }

        public void LogError(string message, string username = "System")
        {
            LogAction("ERROR", message, username);
        }

        // Asynchronous methods
        public async Task LogAsync(string level, string message, string username = "System")
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = level,
                Message = message,
                Username = username
            };

            _context.LogEntries.Add(logEntry);
            await _context.SaveChangesAsync();
        }

        public async Task LogInfoAsync(string message, string username = "System")
        {
            await LogAsync("INFO", message, username);
        }

        public async Task LogWarningAsync(string message, string username = "System")
        {
            await LogAsync("WARNING", message, username);
        }

        public async Task LogErrorAsync(string message, string username = "System")
        {
            await LogAsync("ERROR", message, username);
        }
    }
} 