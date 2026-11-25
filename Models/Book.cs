namespace prj.Models
{
    public class Book : LibraryItem
    {
        public string Author { get; }
        public int PublicationYear { get; }

        public Book(string title, string author, int publicationYear) : base(title)
        {
            Author = author;
            PublicationYear = publicationYear;
        }

        public override string GetDetails()
        {
            return "Title: " + Title + " | Author: " + Author + " | Year: " + PublicationYear;
        }
    }
}
