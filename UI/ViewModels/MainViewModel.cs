using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Welcome.Model;
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
        private readonly User _currentUser;
        private readonly ObservableCollection<User> _users;
        private User? _selectedUser;

        public ICommand AddUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ViewLogsCommand { get; }

        public MainViewModel(IUserService userService, DatabaseContext dbContext, User currentUser)
        {
            _userService = userService;
            _dbContext = dbContext;
            _currentUser = currentUser;
            _users = new ObservableCollection<User>();

            AddUserCommand = new AddUserCommand(this);
            EditUserCommand = new EditUserCommand(this);
            DeleteUserCommand = new DeleteUserCommand(this);
            ViewLogsCommand = new ViewLogsCommand(this);

            LoadUsersAsync().ConfigureAwait(false);
        }

        public ObservableCollection<User> Users => _users;

        public User? SelectedUser
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

                if (_selectedUser.Id == _currentUser.Id)
                {
                    MessageBox.Show("You cannot delete your own account.");
                    return;
                }

                var result = MessageBox.Show($"Are you sure you want to delete user {_selectedUser.Name}?", "Confirm Delete", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    var dbUser = await _dbContext.Users.FindAsync(_selectedUser.Id);
                    if (dbUser != null)
                    {
                        _dbContext.Users.Remove(dbUser);
                        await _dbContext.SaveChangesAsync();
                        _users.Remove(_selectedUser);
                        _selectedUser = null;
                        OnPropertyChanged(nameof(SelectedUser));
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

                var editWindow = new AddEditUserWindow(_userService, _selectedUser);
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