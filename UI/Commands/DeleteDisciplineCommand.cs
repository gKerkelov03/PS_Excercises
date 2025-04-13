using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class DeleteDisciplineCommand : ICommand
    {
        private readonly DisciplineViewModel _viewModel;

        public DeleteDisciplineCommand(DisciplineViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _viewModel.SelectedDiscipline != null;

        public void Execute(object? parameter)
        {
            _viewModel.ExecuteDeleteDiscipline(parameter);
        }
    }
} 