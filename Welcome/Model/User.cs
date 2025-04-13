using Welcome.Others;


namespace Welcome.Model
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }
        
        public DateTime Expires { get; set; }
        
    }
}
