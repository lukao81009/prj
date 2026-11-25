using prj.Models;

namespace prj.Data
{
    public static class SampleBooks
    {
        public static List<Book> GetAll()
        {
            return new List<Book>
            {
                new Book("The Hobbit", "J.R.R. Tolkien", 1937),
                new Book("The Lord of the Rings", "J.R.R. Tolkien", 1954),
                new Book("1984", "George Orwell", 1949),
                new Book("Animal Farm", "George Orwell", 1945),
                new Book("Pride and Prejudice", "Jane Austen", 1813),
                new Book("To Kill a Mockingbird", "Harper Lee", 1960),
                new Book("The Great Gatsby", "F. Scott Fitzgerald", 1925),
                new Book("Moby-Dick", "Herman Melville", 1851),
                new Book("War and Peace", "Leo Tolstoy", 1869),
                new Book("Crime and Punishment", "Fyodor Dostoevsky", 1866),
                new Book("The Catcher in the Rye", "J.D. Salinger", 1951),
                new Book("Brave New World", "Aldous Huxley", 1932),
                new Book("Fahrenheit 451", "Ray Bradbury", 1953),
                new Book("The Little Prince", "Antoine de Saint-Exupery", 1943),
                new Book("Don Quixote", "Miguel de Cervantes", 1605),
                new Book("Hamlet", "William Shakespeare", 1603),
                new Book("Romeo and Juliet", "William Shakespeare", 1597),
                new Book("Frankenstein", "Mary Shelley", 1818),
                new Book("Dracula", "Bram Stoker", 1897),
                new Book("Jane Eyre", "Charlotte Bronte", 1847),
                new Book("Wuthering Heights", "Emily Bronte", 1847),
                new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", 1997),
                new Book("The Alchemist", "Paulo Coelho", 1988),
                new Book("One Hundred Years of Solitude", "Gabriel Garcia Marquez", 1967),
                new Book("The Picture of Dorian Gray", "Oscar Wilde", 1890),
                new Book("Les Miserables", "Victor Hugo", 1862),
                new Book("The Count of Monte Cristo", "Alexandre Dumas", 1844),
                new Book("The Master and Margarita", "Mikhail Bulgakov", 1967),
                new Book("ვეფხისტყაოსანი", "შოთა რუსთაველი", 1200),
                new Book("ჯაყოს ხიზნები", "მიხეილ ჯავახიშვილი", 1924),
                new Book("დიდოსტატის მარჯვენა", "კონსტანტინე გამსახურდია", 1939)
            };
        }
    }
}
