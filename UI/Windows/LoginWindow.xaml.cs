using System.Windows;
using DataLayer.Services;
using DataLayer.Database;
using UI.ViewModels;
using DataLayer;

namespace UI.Windows
{
    public partial class LoginWindow : Window
    {
        private IUserService? _userService;
        private DatabaseContext? _dbContext;

        public LoginWindow()
        {
            InitializeComponent();
        }

        public LoginWindow(IUserService userService, DatabaseContext dbContext) : this()
        {
            _userService = userService;
            _dbContext = dbContext;
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