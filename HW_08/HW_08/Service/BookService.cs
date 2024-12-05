using HW_08.Models;

namespace HW_08.Service
{
    public class BookService
    {
        List<Book> books = new List<Book>();

        public BookService()
        {
            books = new List<Book>()
            {
                new Book { Id = 1, Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = "1949" },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic", Year = "1960" },
                new Book { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", Year = "1925" },
                new Book { Id = 4, Title = "Moby Dick", Author = "Herman Melville", Genre = "Adventure", Year = "1851" },
                new Book { Id = 5, Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Year = "1813" },
                new Book { Id = 6, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Genre = "Classic", Year = "1951" },
                new Book { Id = 7, Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = "1954" },
                new Book { Id = 8, Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", Genre = "Fantasy", Year = "1997" },
                new Book { Id = 9, Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = "1937" },
                new Book { Id = 10, Title = "Fahrenheit 451", Author = "Ray Bradbury", Genre = "Dystopian", Year = "1953" }
            };
        }


        public List<Book> GetBooks() => books;

        public void AddBook(Book book)
        {
            books.Add(book);
        }

    }
}
