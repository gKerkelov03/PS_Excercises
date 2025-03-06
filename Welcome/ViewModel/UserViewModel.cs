using Welcome.Model;
using Welcome.Others;

namespace Welcome.ViewModel
{
    public class UserViewModel
    {
        private User _user;

        public UserViewModel(User user)
        {
            this._user = user;
        }

        public string Name 
        {
            get => this._user.Name;
            set 
            {
                this._user.Name = value; 
            }
        }

        public string Password 
        {
            get => this._user.Password;
            set
            {
                this._user.Password = value;
            }
        }

        public UserRole Role
        {
            get => this._user.Role;
            set
            {
                this._user.Role = value;
            }
        }
    }
}
