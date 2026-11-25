using System.Text.Json;
using Spectre.Console;
using prj.Models;

namespace prj.Storage
{
    public class JsonUserStorage
    {
        private string _filePath;

        private class UserData
        {
            public string Username { get; set; } = "";
            public string Password { get; set; } = "";
            public string Role { get; set; } = "User";
        }

        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public JsonUserStorage(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(List<User> users)
        {
            List<UserData> data = new List<UserData>();

            foreach (User user in users)
            {
                UserData item = new UserData();
                item.Username = user.Username;
                item.Password = user.Password;
                item.Role = user.Role.ToString();
                data.Add(item);
            }

            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(_filePath, json);
        }

        public List<User> Load()
        {
            List<User> users = new List<User>();

            if (!File.Exists(_filePath))
            {
                return users;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                List<UserData>? data = JsonSerializer.Deserialize<List<UserData>>(json, _options);

                if (data != null)
                {
                    foreach (UserData item in data)
                    {
                        UserRole role = UserRole.User;
                        if (item.Role == "Admin")
                        {
                            role = UserRole.Admin;
                        }

                        users.Add(new User(item.Username, item.Password, role));
                    }
                }
            }
            catch (JsonException)
            {
                AnsiConsole.MarkupLine("[yellow]Warning: users.json is damaged. Starting with no users.[/]");
            }

            return users;
        }
    }
}
