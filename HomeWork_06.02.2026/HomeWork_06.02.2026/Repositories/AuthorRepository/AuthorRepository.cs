using HomeWork_06._02._2026.Models;
using HomeWork_06._02._2026.Storage;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_06._02._2026.Repositories.AuthorRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _dataContext;

        public AuthorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _dataContext.Authors
                .Include(a => a.Books)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Author> GetAuthor(int id)
        {
            var author = await _dataContext.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                throw new ArgumentException("Author not found");
            }

            return author;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            _dataContext.Authors.Add(author);
            await _dataContext.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            var foundAuthor = await _dataContext.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == author.Id);

            if (foundAuthor == null)
            {
                throw new ArgumentException("Author not found");
            }

            _dataContext.Entry(foundAuthor).CurrentValues.SetValues(author);

            await _dataContext.SaveChangesAsync();
            return foundAuthor;
        }

        public async Task DeleteAuthor(int id)
        {
            var foundAuthor = await _dataContext.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (foundAuthor == null)
            {
                throw new ArgumentException("Author not found");
            }

            _dataContext.Authors.Remove(foundAuthor);
            await _dataContext.SaveChangesAsync();
        }
    }
}