using prj.Models;
using prj.Services;
using prj.Storage;
using prj.Validation;
using Spectre.Console;

namespace prj.UI
{
    public class ConsoleUI
    {
        private BookManager _manager;
        private JsonBookStorage _storage;
        private AuthService _auth;

        public ConsoleUI(BookManager manager, JsonBookStorage storage, AuthService auth)
        {
            _manager = manager;
            _storage = storage;
            _auth = auth;
        }

        public void Run(User user)
        {
            while (true)
            {
                AnsiConsole.WriteLine();
                AnsiConsole.Write(new Rule("[yellow]Book Manager[/]"));
                AnsiConsole.MarkupLine("Logged in as [green]" + Markup.Escape(user.Username) + "[/] ([blue]" + user.Role + "[/])");

                var prompt = new SelectionPrompt<string>();
                prompt.Title("[bold]What do you want to do?[/]");
                prompt.AddChoices("Show all books", "Search for a book by title", "Show books sorted by year");

                if (user.IsAdmin())
                {
                    prompt.AddChoices("Add a new book (admin)", "Remove a book (admin)", "Add a new admin (admin)");
                }

                prompt.AddChoice("Log out");

                string choice = AnsiConsole.Prompt(prompt);

                if (choice == "Show all books")
                {
                    ShowBooks(_manager.GetAllBooks());
                }
                else if (choice == "Search for a book by title")
                {
                    SearchBook();
                }
                else if (choice == "Show books sorted by year")
                {
                    ShowBooks(_manager.GetBooksSortedByYear());
                }
                else if (choice == "Add a new book (admin)")
                {
                    AddBook();
                }
                else if (choice == "Remove a book (admin)")
                {
                    RemoveBook();
                }
                else if (choice == "Add a new admin (admin)")
                {
                    AddAdmin();
                }
                else
                {
                    AnsiConsole.MarkupLine("[grey]Logged out.[/]");
                    return;
                }
            }
        }

        private void ShowBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]The book list is empty.[/]");
                return;
            }

            Table table = new Table();
            table.Border(TableBorder.Rounded);
            table.BorderColor(Color.Grey);
            table.AddColumn("[bold]#[/]");
            table.AddColumn("[bold]Title[/]");
            table.AddColumn("[bold]Author[/]");
            table.AddColumn(new TableColumn("[bold]Year[/]").RightAligned());

            int number = 1;
            foreach (Book book in books)
            {
                table.AddRow(
                    number.ToString(),
                    "[green]" + Markup.Escape(book.Title) + "[/]",
                    Markup.Escape(book.Author),
                    book.PublicationYear.ToString());
                number++;
            }

            AnsiConsole.Write(table);
        }

        private void SearchBook()
        {
            string text = InputValidator.ReadNonEmpty("Enter title to search:");
            List<Book> results = _manager.SearchByTitle(text);

            if (results.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No book found.[/]");
            }
            else
            {
                ShowBooks(results);
            }
        }

        private void AddBook()
        {
            string title = InputValidator.ReadNonEmpty("Title:");
            string author = InputValidator.ReadNonEmpty("Author:");
            int year = InputValidator.ReadYear("Publication year:");

            Book book = new Book(title, author, year);

            if (_manager.AddBook(book))
            {
                _storage.Save(_manager.GetAllBooks());
                AnsiConsole.MarkupLine("[green]Book added and saved![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]This book already exists in the list.[/]");
            }
        }

        private void RemoveBook()
        {
            string title = InputValidator.ReadNonEmpty("Enter the exact title to remove:");

            if (!AnsiConsole.Confirm("Remove [yellow]" + Markup.Escape(title) + "[/]?"))
            {
                return;
            }

            if (_manager.RemoveBook(title))
            {
                _storage.Save(_manager.GetAllBooks());
                AnsiConsole.MarkupLine("[green]Book removed.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No book with that title was found.[/]");
            }
        }

        private void AddAdmin()
        {
            string username = InputValidator.ReadUsername("New admin username:");

            if (_auth.UsernameExists(username))
            {
                AnsiConsole.MarkupLine("[red]This username is already taken.[/]");
                return;
            }

            string password = InputValidator.ReadNewPassword();
            _auth.Register(username, password, UserRole.Admin);
            AnsiConsole.MarkupLine("[green]Admin " + Markup.Escape(username) + " created.[/]");
        }
    }
}
