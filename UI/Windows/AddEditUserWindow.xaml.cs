using System.Windows;
using DataLayer.Services;
using Welcome.Model;
using Welcome.Others;
using DataLayer.Model;

namespace UI.Windows
{
    public partial class AddEditUserWindow : Window
    {
        private readonly IUserService _userService;
        private readonly User? _existingUser;

        public AddEditUserWindow(IUserService userService, User? existingUser = null)
        {
            InitializeComponent();
            _userService = userService;
            _existingUser = existingUser;

            // Populate role combobox
            RoleComboBox.ItemsSource = Enum.GetValues(typeof(UserRole));
            
            if (_existingUser != null)
            {
                Title = "Edit User";
                UsernameTextBox.Text = _existingUser.Name;
                PasswordBox.Password = _existingUser.Password;
                RoleComboBox.SelectedItem = _existingUser.Role;
                ExpiresDatePicker.SelectedDate = _existingUser.Expires;
            }
            else
            {
                Title = "Add User";
                ExpiresDatePicker.SelectedDate = DateTime.Now.AddYears(1);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ExpiresDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select an expiration date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_existingUser != null)
                {
                    // Get the existing database user to ensure we have the correct ID
                    var existingDbUser = await _userService.GetUserByIdAsync(_existingUser.Id);
                    if (existingDbUser != null)
                    {
                        existingDbUser.Name = UsernameTextBox.Text;
                        existingDbUser.Password = PasswordBox.Password;
                        existingDbUser.Role = (UserRole)RoleComboBox.SelectedItem;
                        existingDbUser.Expires = ExpiresDatePicker.SelectedDate.Value;
                        
                        await _userService.UpdateUserAsync(existingDbUser);
                    }
                    else
                    {
                        MessageBox.Show("User not found in database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    var dbUser = new DatabaseUser
                    {
                        Name = UsernameTextBox.Text,
                        Password = PasswordBox.Password,
                        Role = (UserRole)RoleComboBox.SelectedItem,
                        Expires = ExpiresDatePicker.SelectedDate.Value
                    };
                    
                    await _userService.AddUserAsync(dbUser);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 