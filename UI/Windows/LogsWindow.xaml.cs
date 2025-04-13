using System.Windows;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Windows;

/// <summary>
/// Interaction logic for LogsWindow.xaml
/// </summary>
public partial class LogsWindow : Window
{
    private readonly LogsViewModel _viewModel;

    public LogsWindow()
    {
        InitializeComponent();
        _viewModel = new LogsViewModel();
        DataContext = _viewModel;
    }

    public void AddLog(DataLayer.Model.LogEntry log)
    {
        _viewModel.AddLog(log);
    }

    private void LogsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (LogsDataGrid.SelectedItem is DataLayer.Model.LogEntry log)
        {
            _viewModel.ShowLogDetails(log);
        }
    }
} 