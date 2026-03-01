using CryptoProj.Domain.Abstractions;
using CryptoProj.Domain.Models;
using CryptoProj.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Services.Cats
{
    public class CatsService
    {
        private readonly ICatsRepository _catsRepository;

        public CatsService(ICatsRepository repository)
        {
            _catsRepository = repository;
        }

        public async Task<List<Cat>> GetAll(CatsRequest request)
        {
            return await _catsRepository.GetAll(request);
        }

        public async Task<Cat?> Get(int id)
        {
            return await _catsRepository.Get(id);
        }

        public async Task<Cat> Add(Cat cat)
        {
            return await _catsRepository.Add(cat);
        }
        
        public async Task<Cat> Update(int id, Cat cat)
        {
            return await _catsRepository.Update(id, cat);
        }

        public async Task Delete(int id)
        {
            await _catsRepository.Delete(id);
        }
    }
}