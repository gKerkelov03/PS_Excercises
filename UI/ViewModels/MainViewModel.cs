using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using DataLayer.Services;
using UI.Windows;
using DataLayer.Model;
using DataLayer.Database;
using Microsoft.EntityFrameworkCore;
using Welcome.Others;
using System.Windows.Input;
using UI.Commands;
using DataLayer;
using DataLayer.Repositories;

namespace UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly DatabaseContext _dbContext;
        private readonly DatabaseUser _currentUser;
        private readonly ObservableCollection<DatabaseUser> _users;
        private readonly Logger _logger;
        private readonly DisciplineService _disciplineService;
        private DatabaseUser? _selectedUser;

        public ICommand AddUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ViewLogsCommand { get; }
        public ICommand ManageDisciplinesCommand { get; }

        public MainViewModel(IUserService userService, DatabaseContext dbContext, DatabaseUser currentUser, Logger logger)
        {
            _userService = userService;
            _dbContext = dbContext;
            _currentUser = currentUser;
            _logger = logger;
            _disciplineService = new DisciplineService(new DisciplineRepository(_dbContext));
            _users = new ObservableCollection<DatabaseUser>();

            AddUserCommand = new AddUserCommand(this);
            EditUserCommand = new EditUserCommand(this);
            DeleteUserCommand = new DeleteUserCommand(this);
            ViewLogsCommand = new ViewLogsCommand(this);
            ManageDisciplinesCommand = new ManageDisciplinesCommand(this);

            LoadUsersAsync().ConfigureAwait(false);
        }

        public ObservableCollection<DatabaseUser> Users => _users;

        public DatabaseUser? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                _users.Clear();
                foreach (var user in users)
                {
                    _users.Add(user);
                }
                _logger.LogInfo("Loaded all users", _currentUser.Username);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError($"Error loading users: {ex.Message}", _currentUser.Username);
            }
        }

        public async Task AddUserAsync()
        {
            try
            {
                var addWindow = new AddEditUserWindow(_userService);
                if (addWindow.ShowDialog() == true)
                {
                    await LoadUsersAsync();
                    _logger.LogInfo("Added new user", _currentUser.Username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError($"Error adding user: {ex.Message}", _currentUser.Username);
            }
        }

        public async Task DeleteUserAsync()
        {
            try
            {
                if (_selectedUser == null)
                {
                    MessageBox.Show("Please select a user to delete.");
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete user {_selectedUser.Username}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _userService.DeleteUserAsync(_selectedUser);
                    _logger.LogInfo($"Deleted user: {_selectedUser.Username}", _currentUser.Username);
                    await LoadUsersAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError($"Error deleting user: {ex.Message}", _currentUser.Username);
            }
        }

        public async Task EditUserAsync()
        {   
            try
            {
                if (_selectedUser == null)
                {
                    MessageBox.Show("Please select a user to edit.");
                    return;
                }

                var dbUser = await _userService.GetUserByIdAsync(_selectedUser.Id);
                if (dbUser == null)
                {
                    MessageBox.Show("User not found in database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var editWindow = new AddEditUserWindow(_userService, dbUser);
                if (editWindow.ShowDialog() == true)
                {
                    await LoadUsersAsync();
                    _logger.LogInfo($"Edited user: {dbUser.Username}", _currentUser.Username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError($"Error editing user: {ex.Message}", _currentUser.Username);
            }
        }

        public async Task ViewLogsAsync()
        {
            try
            {
                var logs = await _dbContext.LogEntries
                    .OrderByDescending(l => l.Timestamp)
                    .ToListAsync();

                var logsWindow = new LogsWindow();
                foreach (var log in logs)
                {
                    logsWindow.AddLog(log);
                }
                logsWindow.Show();
                _logger.LogInfo("Viewed logs", _currentUser.Username);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing logs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError($"Error viewing logs: {ex.Message}", _currentUser.Username);
            }
        }

        public async Task ManageDisciplinesAsync()
        {
            try
            {
                var disciplineViewModel = new DisciplineManagementViewModel(
                    _disciplineService,
                    _logger,
                    _currentUser.Username);
                var window = new DisciplineManagementWindow(disciplineViewModel);
                window.Show();
                _logger.LogInfo("Opened discipline management window", _currentUser.Username);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening discipline management: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError($"Error opening discipline management: {ex.Message}", _currentUser.Username);
            }
        }
    }
} 