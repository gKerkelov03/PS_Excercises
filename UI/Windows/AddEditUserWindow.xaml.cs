using System.Windows;
using DataLayer.Services;
using Welcome.Others;
using DataLayer.Model;

namespace UI.Windows
{
    public partial class AddEditUserWindow : Window
    {
        private readonly IUserService _userService;
        private readonly DatabaseUser? _existingUser;

        public AddEditUserWindow(IUserService userService, DatabaseUser? existingUser = null)
        {
            InitializeComponent();
            _userService = userService;
            _existingUser = existingUser;

            // Populate role combobox
            RoleComboBox.ItemsSource = Enum.GetValues(typeof(UserRole));
            
            if (_existingUser != null)
            {
                Title = "Edit User";
                UsernameTextBox.Text = _existingUser.Username;
                PasswordBox.Password = _existingUser.Password;
                RoleComboBox.SelectedItem = _existingUser.Role;
                ExpiresDatePicker.SelectedDate = _existingUser.Expires;
                FacultyNumberTextBox.Text = _existingUser.FacultyNumber;
                EmailTextBox.Text = _existingUser.Email;
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

            if (string.IsNullOrWhiteSpace(FacultyNumberTextBox.Text))
            {
                MessageBox.Show("Please enter a faculty number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Please enter an email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_existingUser != null)
                {
                    // Get the existing user from the database
                    var existingDbUser = await _userService.GetUserByIdAsync(_existingUser.Id);
                    if (existingDbUser == null)
                    {
                        MessageBox.Show("User not found in database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Update the existing user's properties
                    existingDbUser.Username = UsernameTextBox.Text;
                    existingDbUser.Password = PasswordBox.Password;
                    existingDbUser.Role = (UserRole)RoleComboBox.SelectedItem;
                    existingDbUser.Expires = ExpiresDatePicker.SelectedDate.Value;
                    existingDbUser.FacultyNumber = FacultyNumberTextBox.Text;
                    existingDbUser.Email = EmailTextBox.Text;
                    
                    await _userService.UpdateUserAsync(existingDbUser);
                }
                else
                {
                    // Create new user
                    var newUser = new DatabaseUser
                    {
                        Username = UsernameTextBox.Text,
                        Password = PasswordBox.Password,
                        Role = (UserRole)RoleComboBox.SelectedItem,
                        Expires = ExpiresDatePicker.SelectedDate.Value,
                        FacultyNumber = FacultyNumberTextBox.Text,
                        Email = EmailTextBox.Text
                    };
                    
                    await _userService.AddUserAsync(newUser);
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