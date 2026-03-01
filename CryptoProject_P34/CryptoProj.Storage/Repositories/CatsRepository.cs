using CryptoProj.Domain.Abstractions;
using CryptoProj.Domain.Models;
using CryptoProj.Domain.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Storage.Repositories
{
    public class CatsRepository : BaseRepository<Cat>, ICatsRepository
    {
        public CatsRepository(CryptoContext context) : base(context)
        {

        }

        public async Task<List<Cat>> GetAll(CatsRequest request)
        {
            var query = Context.Set<Cat>()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(c => c.Name.Contains(request.Name));
            }

            return await query
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync();
        }

        public async Task<Cat> Update(int id, Cat cat)
        {
            Context.Update(cat);
            await Context.SaveChangesAsync();
            return cat;
        }

        public async Task Delete(int id)
        {
            Cat? foundCat = Context.Cats.FirstOrDefault(c => c.Id == id);

            if (foundCat != null)
            {
                Context.Remove(foundCat);
                await Context.SaveChangesAsync();
            }
            throw new Exception("Cat with this id not found!");
        }
    }
}