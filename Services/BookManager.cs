using prj.Models;

namespace prj.Services
{
    public class BookManager
    {
        private List<Book> _books = new List<Book>();

        public bool AddBook(Book newBook)
        {
            foreach (Book book in _books)
            {
                bool sameTitle = string.Equals(book.Title, newBook.Title, StringComparison.OrdinalIgnoreCase);
                bool sameAuthor = string.Equals(book.Author, newBook.Author, StringComparison.OrdinalIgnoreCase);

                if (sameTitle && sameAuthor)
                {
                    return false;
                }
            }

            _books.Add(newBook);
            return true;
        }

        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books);
        }

        public List<Book> SearchByTitle(string text)
        {
            List<Book> results = new List<Book>();

            foreach (Book book in _books)
            {
                if (book.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(book);
                }
            }

            return results;
        }

        public bool RemoveBook(string title)
        {
            for (int i = 0; i < _books.Count; i++)
            {
                if (string.Equals(_books[i].Title, title, StringComparison.OrdinalIgnoreCase))
                {
                    _books.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public List<Book> GetBooksSortedByYear()
        {
            List<Book> sorted = new List<Book>(_books);
            sorted.Sort((a, b) => a.PublicationYear.CompareTo(b.PublicationYear));
            return sorted;
        }
    }
}
