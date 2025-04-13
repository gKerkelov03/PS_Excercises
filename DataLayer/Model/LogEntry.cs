using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model;

public class LogEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Action { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
    public string Username { get; set; }
} 