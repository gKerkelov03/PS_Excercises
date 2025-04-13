using System.Collections.ObjectModel;
using System.Windows;
using DataLayer.Model;

namespace UI.ViewModels
{
    public class LogsViewModel : ViewModelBase
    {
        private ObservableCollection<LogEntry> _logs;
        private LogEntry _selectedLog;

        public LogsViewModel()
        {
            _logs = new ObservableCollection<LogEntry>();
        }

        public ObservableCollection<LogEntry> Logs
        {
            get => _logs;
            private set => SetProperty(ref _logs, value);
        }

        public LogEntry SelectedLog
        {
            get => _selectedLog;
            set => SetProperty(ref _selectedLog, value);
        }

        public void AddLog(LogEntry log)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _logs.Add(log);
            });
        }

        public void ShowLogDetails(LogEntry log)
        {
            if (log != null)
            {
                var message = $"Date: {log.Timestamp:dd.MM.yyyy HH:mm:ss}\n" +
                             $"Level: {log.Level}\n" +
                             $"Message: {log.Message}\n" +
                             $"Username: {log.Username}";
                
                MessageBox.Show(message, "Log Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
} 