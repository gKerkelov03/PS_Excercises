using System.Windows;
using DataLayer.Services;
using UI.Commands;
using UI.Windows;
using Welcome.Model;

namespace UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private string _username;
        private string _password;
        private LoginCommand _loginCommand;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
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
                var mainWindow = new MainWindow(_userService, user);
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