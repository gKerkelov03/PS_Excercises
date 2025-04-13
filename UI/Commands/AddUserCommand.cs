using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class AddUserCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public AddUserCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public async void Execute(object? parameter)
        {
            await _viewModel.AddUserAsync();
        }
    }
} 