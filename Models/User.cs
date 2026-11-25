namespace prj.Models
{
    public class User
    {
        public string Username { get; }
        public string Password { get; set; }
        public UserRole Role { get; }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public bool IsAdmin()
        {
            return Role == UserRole.Admin;
        }
    }
}
