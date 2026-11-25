using Spectre.Console;

namespace prj.Validation
{
    public static class InputValidator
    {
        public static string ReadNonEmpty(string message)
        {
            var prompt = new TextPrompt<string>(message)
                .Validate(text => string.IsNullOrWhiteSpace(text)
                    ? ValidationResult.Error("[red]This field cannot be empty![/]")
                    : ValidationResult.Success());

            return AnsiConsole.Prompt(prompt).Trim();
        }

        public static int ReadYear(string message)
        {
            int currentYear = DateTime.Now.Year;

            var prompt = new TextPrompt<int>(message)
                .ValidationErrorMessage("[red]Enter a valid number![/]")
                .Validate(year => year > 0 && year <= currentYear
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Year must be between 1 and " + currentYear + "![/]"));

            return AnsiConsole.Prompt(prompt);
        }

        public static string ReadUsername(string message)
        {
            var prompt = new TextPrompt<string>(message)
                .Validate(name => name.Length >= 3 && !name.Contains(' ')
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]At least 3 characters and no spaces![/]"));

            return AnsiConsole.Prompt(prompt).Trim();
        }

        public static string ReadPassword(string message)
        {
            var prompt = new TextPrompt<string>(message).Secret();
            return AnsiConsole.Prompt(prompt);
        }

        public static string ReadNewPassword()
        {
            while (true)
            {
                var prompt = new TextPrompt<string>("Password (min 6 characters):")
                    .Secret()
                    .Validate(pass => pass.Length >= 6
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Password is too short![/]"));

                string password = AnsiConsole.Prompt(prompt);
                string repeat = ReadPassword("Repeat password:");

                if (password == repeat)
                {
                    return password;
                }

                AnsiConsole.MarkupLine("[red]Passwords do not match![/]");
            }
        }
    }
}
