using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class EditDisciplineCommand : ICommand
    {
        private readonly DisciplineViewModel _viewModel;

        public EditDisciplineCommand(DisciplineViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _viewModel.SelectedDiscipline != null;

        public void Execute(object? parameter)
        {
            _viewModel.ExecuteEditDiscipline(parameter);
        }
    }
} 