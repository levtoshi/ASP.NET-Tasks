using HomeWork_06._02._2026.Models;
using HomeWork_06._02._2026.Repositories.BookRepository;
using HomeWork_06._02._2026.Services.ValidDescriptionService;

namespace HomeWork_06._02._2026.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidDescriptionService _validDescriptionService;

        public BookService(IBookRepository bookRepository,
            IValidDescriptionService validDescriptionService)
        {
            _bookRepository = bookRepository;
            _validDescriptionService = validDescriptionService;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetBooks();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _bookRepository.GetBook(id);
        }

        public async Task<Book> AddBook(Book book)
        {
            if (_validDescriptionService.IsValidDescription(book.Description))
            {
                return await _bookRepository.AddBook(book);
            }
            throw new Exception("Description is not valid");
        }

        public async Task<Book> UpdateBook(Book book)
        {
            if (_validDescriptionService.IsValidDescription(book.Description))
            {
                return await _bookRepository.UpdateBook(book);
            }
            throw new Exception("Description is not valid");
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }
    }
}