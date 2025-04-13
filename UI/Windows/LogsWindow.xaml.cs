using System.Windows;
using System.Windows.Input;
using UI.ViewModels;
using DataLayer.Model;
using System.Collections.ObjectModel;

namespace UI.Windows;

/// <summary>
/// Interaction logic for LogsWindow.xaml
/// </summary>
public partial class LogsWindow : Window
{
    private readonly LogsViewModel _viewModel;
    private readonly ObservableCollection<LogEntry> _logs;

    public LogsWindow()
    {
        InitializeComponent();
        _viewModel = new LogsViewModel();
        DataContext = _viewModel;
        _logs = new ObservableCollection<LogEntry>();
        LogsDataGrid.ItemsSource = _logs;
    }

    public void AddLog(LogEntry log)
    {
        _logs.Add(log);
    }

    private void LogsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (LogsDataGrid.SelectedItem is LogEntry log)
        {
            _viewModel.ShowLogDetails(log);
        }
    }
} 