using System.Windows;
using DataLayer.Services;
using UI.ViewModels;

namespace UI.Windows
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow(IUserService userService)
        {
            InitializeComponent();
            _viewModel = new LoginViewModel(userService);
            DataContext = _viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
} 