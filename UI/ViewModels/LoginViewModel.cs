using System.Windows;
using System.Windows.Input;
using DataLayer.Services;
using DataLayer.Model;
using DataLayer.Database;
using UI.Windows;
using UI.Commands;
using DataLayer;

namespace UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly DatabaseContext _dbContext;
        private readonly Logger _logger;
        private string _username = string.Empty;
        private string _password = string.Empty;

        public ICommand LoginCommand { get; }

        public LoginViewModel(IUserService userService, DatabaseContext dbContext, Logger logger)
        {
            _userService = userService;
            _dbContext = dbContext;
            _logger = logger;
            LoginCommand = new LoginCommand(this);
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var isValid = await _userService.ValidateUserAsync(Username, Password);
                if (isValid)
                {
                    var user = await _userService.GetUserByNameAsync(Username);
                    if (user != null)
                    {
                        _logger.LogInfo($"User {Username} logged in successfully", Username);
                        var mainWindow = new MainWindow(_userService, _dbContext, user);
                        mainWindow.Show();
                        Application.Current.MainWindow.Close();
                    }
                }
                else
                {
                    _logger.LogWarning($"Failed login attempt for user {Username}", Username);
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during login: {ex.Message}", Username);
                MessageBox.Show($"Error during login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 