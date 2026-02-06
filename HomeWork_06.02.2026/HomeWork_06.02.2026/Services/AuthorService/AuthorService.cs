using HomeWork_06._02._2026.Models;
using HomeWork_06._02._2026.Repositories.AuthorRepository;

namespace HomeWork_06._02._2026.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _authorRepository.GetAuthors();
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await _authorRepository.GetAuthor(id);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            return await _authorRepository.AddAuthor(author);
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            return await _authorRepository.UpdateAuthor(author);
        }

        public async Task DeleteAuthor(int id)
        {
            await _authorRepository.DeleteAuthor(id);
        }
    }
}