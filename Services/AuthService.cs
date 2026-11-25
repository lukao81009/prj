using prj.Models;
using prj.Storage;

namespace prj.Services
{
    public class AuthService
    {
        private List<User> _users;
        private JsonUserStorage _storage;

        private Dictionary<string, string> _resetCodes = new Dictionary<string, string>();

        public AuthService(JsonUserStorage storage)
        {
            _storage = storage;
            _users = storage.Load();
        }

        public bool EnsureDefaultAdmin()
        {
            if (_users.Count > 0)
            {
                return false;
            }

            Register("admin", "admin123", UserRole.Admin);
            return true;
        }

        private User? FindUser(string username)
        {
            foreach (User user in _users)
            {
                if (string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }

            return null;
        }

        public bool UsernameExists(string username)
        {
            return FindUser(username) != null;
        }

        public User? Register(string username, string password, UserRole role)
        {
            if (UsernameExists(username))
            {
                return null;
            }

            User newUser = new User(username, password, role);
            _users.Add(newUser);
            _storage.Save(_users);
            return newUser;
        }

        public User? Login(string username, string password)
        {
            User? user = FindUser(username);

            if (user != null && user.Password == password)
            {
                return user;
            }

            return null;
        }

        public string? CreateResetCode(string username)
        {
            User? user = FindUser(username);

            if (user == null)
            {
                return null;
            }

            string code = Random.Shared.Next(100000, 1000000).ToString();
            _resetCodes[user.Username.ToLower()] = code;
            return code;
        }

        public bool ResetPassword(string username, string code, string newPassword)
        {
            User? user = FindUser(username);

            if (user == null)
            {
                return false;
            }

            string key = user.Username.ToLower();

            if (!_resetCodes.ContainsKey(key) || _resetCodes[key] != code)
            {
                return false;
            }

            user.Password = newPassword;
            _resetCodes.Remove(key);
            _storage.Save(_users);
            return true;
        }
    }
}
