using HomeWork_06._03._2026.Models;

namespace HomeWork_06._03._2026.Services
{
    public class BookService
    {
        public List<Book> Books { get; set; } = new();

        public BookService()
        {
            for (int i = 1; i <= 25; i++)
            {
                Books.Add(new Book
                {
                    Id = i,
                    Title = $"Book Title {i}",
                    Author = "Author Name",
                    Price = 250 + (i * 10),
                    Description = "This is a detailed description of the book. It provides insights into the plot and characters."
                });
            }
        }
    }
}