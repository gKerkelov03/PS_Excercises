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

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true; // Always return true to make the button clickable
        }

        public void Execute(object parameter)
        {
            if (_viewModel.SelectedDiscipline == null)
            {
                System.Windows.MessageBox.Show(
                    "Please select a discipline to delete.",
                    "No Discipline Selected",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return;
            }
            _viewModel.ExecuteDeleteDiscipline(parameter);
        }
    }
} 