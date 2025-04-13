using System;

namespace DataLayer.Model;

public class LogEntry
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public required string Level { get; set; }
    public required string Message { get; set; }
    public required string Username { get; set; }
} 