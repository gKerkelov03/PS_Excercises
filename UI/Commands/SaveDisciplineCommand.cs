using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class SaveDisciplineCommand : ICommand
    {
        private readonly DisciplineEditViewModel _viewModel;

        public SaveDisciplineCommand(DisciplineEditViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public async void Execute(object? parameter)
        {
            await _viewModel.SaveAsync();
        }
    }
} 