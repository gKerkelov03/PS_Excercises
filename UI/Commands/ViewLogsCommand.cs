using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class ViewLogsCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public ViewLogsCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public async void Execute(object? parameter)
        {
            await _viewModel.ViewLogsAsync();
        }
    }
} 