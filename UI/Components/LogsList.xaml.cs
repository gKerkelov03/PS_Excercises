using System.Windows.Controls;
using System.Collections.ObjectModel;
using DataLayer.Model;

namespace UI.Components;

public partial class LogsList : UserControl
{
    private ObservableCollection<LogEntry> _logs;

    public LogsList()
    {
        InitializeComponent();
        _logs = new ObservableCollection<LogEntry>();
        LogsDataGrid.ItemsSource = _logs;
    }

    public void AddLog(LogEntry log)
    {
        _logs.Add(log);
        LogsDataGrid.ScrollIntoView(_logs[_logs.Count - 1]);
    }

    public void UpdateLogView(LogEntry log)
    {
        _logs.Add(new LogEntry
        {
            Timestamp = log.Timestamp,
            Level = log.Level,
            Message = log.Message,
            Username = log.Username
        });
    }
} 