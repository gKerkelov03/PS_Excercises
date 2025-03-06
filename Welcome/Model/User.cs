using Welcome.Others;


namespace Welcome.Model
{
    public class User
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }
        
        public int Id { get; set; }
        
        public DateTime Expires { get; set; }
    }
}
