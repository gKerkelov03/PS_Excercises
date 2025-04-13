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
                    "Please select a discipline to edit.",
                    "No Discipline Selected",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return;
            }
            _viewModel.ExecuteEditDiscipline(parameter);
        }
    }
} 