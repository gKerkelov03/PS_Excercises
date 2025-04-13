using System.Windows;
using DataLayer.Services;
using DataLayer.Database;
using UI.ViewModels;
using DataLayer;

namespace UI.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(IUserService userService, DatabaseContext dbContext)
        {
            InitializeComponent();
            var logger = new Logger(dbContext);
            DataContext = new LoginViewModel(userService, dbContext, logger);
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