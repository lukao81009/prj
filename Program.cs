using prj.Data;
using prj.Models;
using prj.Services;
using prj.Storage;
using prj.UI;
using Spectre.Console;

namespace prj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string booksPath = Path.Combine(AppContext.BaseDirectory, "books.json");
            JsonBookStorage bookStorage = new JsonBookStorage(booksPath);

            BookManager bookManager = new BookManager();
            foreach (Book book in bookStorage.Load())
            {
                bookManager.AddBook(book);
            }

            if (bookManager.GetAllBooks().Count == 0)
            {
                foreach (Book book in SampleBooks.GetAll())
                {
                    bookManager.AddBook(book);
                }
                bookStorage.Save(bookManager.GetAllBooks());
            }

            string usersPath = Path.Combine(AppContext.BaseDirectory, "users.json");
            AuthService auth = new AuthService(new JsonUserStorage(usersPath));

            if (auth.EnsureDefaultAdmin())
            {
                AnsiConsole.MarkupLine("[yellow]First run: admin created -> username: admin, password: admin123[/]");
            }

            AuthUI loginScreen = new AuthUI(auth);
            ConsoleUI menuScreen = new ConsoleUI(bookManager, bookStorage, auth);

            while (true)
            {
                User? user = loginScreen.Show();

                if (user == null)
                {
                    break;
                }

                menuScreen.Run(user);
            }
        }
    }
}
