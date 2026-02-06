using HomeWork_06._02._2026.Models;

namespace HomeWork_06._02._2026.Repositories.BookRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int id);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task DeleteBook(int id);
    }
}