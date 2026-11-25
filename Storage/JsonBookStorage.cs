using System.Text.Json;
using Spectre.Console;
using prj.Models;

namespace prj.Storage
{
    public class JsonBookStorage
    {
        private string _filePath;

        private class BookData
        {
            public string Title { get; set; } = "";
            public string Author { get; set; } = "";
            public int PublicationYear { get; set; }
        }

        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public JsonBookStorage(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(List<Book> books)
        {
            List<BookData> data = new List<BookData>();

            foreach (Book book in books)
            {
                BookData item = new BookData();
                item.Title = book.Title;
                item.Author = book.Author;
                item.PublicationYear = book.PublicationYear;
                data.Add(item);
            }

            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(_filePath, json);
        }

        public List<Book> Load()
        {
            List<Book> books = new List<Book>();

            if (!File.Exists(_filePath))
            {
                return books;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                List<BookData>? data = JsonSerializer.Deserialize<List<BookData>>(json, _options);

                if (data != null)
                {
                    foreach (BookData item in data)
                    {
                        books.Add(new Book(item.Title, item.Author, item.PublicationYear));
                    }
                }
            }
            catch (JsonException)
            {
                AnsiConsole.MarkupLine("[yellow]Warning: books.json is damaged. Starting with an empty list.[/]");
            }

            return books;
        }
    }
}
