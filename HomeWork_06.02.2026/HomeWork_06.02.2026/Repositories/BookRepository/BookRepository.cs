using HomeWork_06._02._2026.Models;
using HomeWork_06._02._2026.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_06._02._2026.Repositories.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _dataContext.Books
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Book> GetBook(int id)
        {
            var book = await _dataContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }
            return book;
        }

        public async Task<Book> AddBook(Book book)
        {
            var foundAuthor = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId);

            if (foundAuthor == null)
            {
                throw new ArgumentException("Author not found");
            }

            _dataContext.Add(book);
            await _dataContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var foundBook = await _dataContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            var foundAuthor = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId);

            if (foundBook == null)
            {
                throw new ArgumentException("Book not found");
            }

            if (foundAuthor == null)
            {
                throw new ArgumentException("Author not found");
            }

            _dataContext.Entry(foundBook).CurrentValues.SetValues(book);

            await _dataContext.SaveChangesAsync();
            return foundBook;
        }

        public async Task DeleteBook(int id)
        {
            var foundBook = await _dataContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (foundBook == null)
            {
                throw new ArgumentException("Book not found");
            }

            _dataContext.Books.Remove(foundBook);
            await _dataContext.SaveChangesAsync();
        }
    }
}