using prj.Models;
using prj.Services;
using prj.Validation;
using Spectre.Console;

namespace prj.UI
{
    public class AuthUI
    {
        private AuthService _auth;

        public AuthUI(AuthService auth)
        {
            _auth = auth;
        }

        public User? Show()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new FigletText("Book Manager").Color(Color.Yellow));

            while (true)
            {
                var prompt = new SelectionPrompt<string>();
                prompt.Title("[bold]Welcome![/]");
                prompt.AddChoices("Log in", "Sign up", "Exit");

                string choice = AnsiConsole.Prompt(prompt);

                if (choice == "Log in")
                {
                    User? user = LogIn();
                    if (user != null)
                    {
                        return user;
                    }
                }
                else if (choice == "Sign up")
                {
                    User? user = SignUp();
                    if (user != null)
                    {
                        return user;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private User? LogIn()
        {
            string username = InputValidator.ReadNonEmpty("Username:");
            string password = InputValidator.ReadPassword("Password:");

            User? user = _auth.Login(username, password);

            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Wrong username or password.[/]");
                return null;
            }

            AnsiConsole.MarkupLine("[green]Welcome, " + Markup.Escape(user.Username) + "![/]");
            return user;
        }

        private User? SignUp()
        {
            string username = InputValidator.ReadUsername("Choose a username:");

            if (_auth.UsernameExists(username))
            {
                AnsiConsole.MarkupLine("[red]This username is already taken.[/]");
                return null;
            }

            string password = InputValidator.ReadNewPassword();
            User? user = _auth.Register(username, password, UserRole.User);

            AnsiConsole.MarkupLine("[green]Account created! You are now logged in.[/]");
            return user;
        }
    }
}
