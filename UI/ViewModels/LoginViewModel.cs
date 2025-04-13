using System.Windows;
using DataLayer.Services;
using UI.Commands;
using UI.Windows;
using Welcome.Model;
using DataLayer.Database;

namespace UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly DatabaseContext _dbContext;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private readonly LoginCommand _loginCommand;

        public LoginViewModel(IUserService userService, DatabaseContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
            _loginCommand = new LoginCommand(this);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public LoginCommand LoginCommand => _loginCommand;

        public async void Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var isValid = await _userService.ValidateUserAsync(Username, Password);
            if (isValid)
            {
                var user = await _userService.GetUserByNameAsync(Username);
                var mainWindow = new MainWindow(_userService, _dbContext, user);
                mainWindow.Show();
                
                // Close the login window
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 