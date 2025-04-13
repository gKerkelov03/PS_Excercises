using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;

        public LoginCommand(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.Login();
        }
    }
} 