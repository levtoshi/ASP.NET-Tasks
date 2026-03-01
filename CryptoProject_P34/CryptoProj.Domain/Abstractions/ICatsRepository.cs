using CryptoProj.Domain.Models;
using CryptoProj.Domain.Models.Requests;
using CryptoProj.Domain.Services.Cats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Abstractions
{
    public interface ICatsRepository
    {
        Task<List<Cat>> GetAll(CatsRequest request);
        ValueTask<Cat?> Get(int id);
        Task<Cat> Add(Cat cat);
        Task<Cat> Update(int id, Cat cat);
        Task Delete(int id);
    }
}