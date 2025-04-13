using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class RefreshCommand : ICommand
    {
        private readonly DisciplineViewModel _viewModel;

        public RefreshCommand(DisciplineViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _viewModel.ExecuteRefresh(parameter);
        }
    }
} 