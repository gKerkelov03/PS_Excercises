using System.Windows;
using DataLayer.Services;
using UI.ViewModels;
using DataLayer.Database;

namespace UI.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(IUserService userService, DatabaseContext dbContext)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(userService, dbContext);
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