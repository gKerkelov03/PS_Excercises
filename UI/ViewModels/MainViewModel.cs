using System.Collections.ObjectModel;
using System.Windows;
using DataLayer.Services;
using UI.Commands;
using UI.Windows;
using Welcome.Model;
using DataLayer.Model;

namespace UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly User _currentUser;
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private AddUserCommand _addUserCommand;
        private EditUserCommand _editUserCommand;
        private DeleteUserCommand _deleteUserCommand;
        private ViewLogsCommand _viewLogsCommand;

        public MainViewModel(IUserService userService, User currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
            _users = new ObservableCollection<User>();
            _addUserCommand = new AddUserCommand(this);
            _editUserCommand = new EditUserCommand(this);
            _deleteUserCommand = new DeleteUserCommand(this);
            _viewLogsCommand = new ViewLogsCommand(this);
            
            LoadUsersAsync();
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            private set => SetProperty(ref _users, value);
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        public AddUserCommand AddUserCommand => _addUserCommand;
        public EditUserCommand EditUserCommand => _editUserCommand;
        public DeleteUserCommand DeleteUserCommand => _deleteUserCommand;
        public ViewLogsCommand ViewLogsCommand => _viewLogsCommand;

        public async Task LoadUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        public async Task AddUserAsync()
        {
            var addUserWindow = new AddEditUserWindow(_userService);
            if (addUserWindow.ShowDialog() == true)
            {
                await LoadUsersAsync();
            }
        }

        public async Task EditUserAsync()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to edit.", "Edit User", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editUserWindow = new AddEditUserWindow(_userService, SelectedUser);
            if (editUserWindow.ShowDialog() == true)
            {
                await LoadUsersAsync();
            }
        }

        public async Task DeleteUserAsync()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.", "Delete User", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete user {SelectedUser.Name}?", "Confirm Delete", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                try 
                {
                    // Get the database user directly from the service to ensure we have the correct ID
                    var dbUser = await _userService.GetUserByIdAsync(SelectedUser.Id);
                    if (dbUser != null)
                    {
                        await _userService.DeleteUserAsync(dbUser);
                        await LoadUsersAsync();
                    }
                    else
                    {
                        MessageBox.Show("User not found in database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public async Task ViewLogsAsync()
        {
            var logsWindow = new LogsWindow();
            logsWindow.Show();
        }
    }
} 