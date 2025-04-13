using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class AddDisciplineCommand : ICommand
    {
        private readonly DisciplineViewModel _viewModel;

        public AddDisciplineCommand(DisciplineViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _viewModel.ExecuteAddDiscipline(parameter);
        }
    }
} 