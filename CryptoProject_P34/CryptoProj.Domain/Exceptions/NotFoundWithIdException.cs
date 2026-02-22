using CryptoProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Exceptions
{
    public class NotFoundWithIdException : Exception
    {
        public NotFoundWithIdException(int id) : base($"Item with id {id} not found")
        {
        }
    }
}