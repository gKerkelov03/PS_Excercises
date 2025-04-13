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

namespace UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly DatabaseContext _dbContext;
        private readonly DatabaseUser _currentUser;
        private readonly ObservableCollection<DatabaseUser> _users;
        private DatabaseUser? _selectedUser;

        public ICommand AddUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ViewLogsCommand { get; }

        public MainViewModel(IUserService userService, DatabaseContext dbContext, DatabaseUser currentUser)
        {
            _userService = userService;
            _dbContext = dbContext;
            _currentUser = currentUser;
            _users = new ObservableCollection<DatabaseUser>();

            AddUserCommand = new AddUserCommand(this);
            EditUserCommand = new EditUserCommand(this);
            DeleteUserCommand = new DeleteUserCommand(this);
            ViewLogsCommand = new ViewLogsCommand(this);

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

        public async Task LoadUsersAsync()
        {
            try
            {
                _users.Clear();
                var users = await _userService.GetAllUsersAsync();
                foreach (var user in users)
                {
                    _users.Add(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                if (_selectedUser.Username == _currentUser.Username)
                {
                    MessageBox.Show("You cannot delete your own account.");
                    return;
                }

                var result = MessageBox.Show($"Are you sure you want to delete user {_selectedUser.Username}?", "Confirm Delete", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    var dbUser = await _userService.GetUserByIdAsync(_selectedUser.Id);
                    if (dbUser != null)
                    {
                        await _userService.DeleteUserAsync(dbUser);
                        _users.Remove(_selectedUser);
                        _selectedUser = null;
                        OnPropertyChanged(nameof(SelectedUser));
                    }
                    else
                    {
                        MessageBox.Show("User not found in database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing logs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 