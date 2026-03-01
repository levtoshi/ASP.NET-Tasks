using CryptoProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Abstractions
{
    public interface INewsRepository
    {
        Task<News[]> GetAll();
        ValueTask<News?> Get(int id);
        Task<News> Add(News entity);
        Task<News> Update(News entity);
        Task Delete (int id);
    }
}