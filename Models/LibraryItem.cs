namespace prj.Models
{
    public abstract class LibraryItem
    {
        public string Title { get; }

        public LibraryItem(string title)
        {
            Title = title;
        }

        public abstract string GetDetails();
    }
}
