using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class CancelCommand : ICommand
    {
        private readonly DisciplineEditViewModel _viewModel;

        public CancelCommand(DisciplineEditViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _viewModel.Cancel();
        }
    }
} 