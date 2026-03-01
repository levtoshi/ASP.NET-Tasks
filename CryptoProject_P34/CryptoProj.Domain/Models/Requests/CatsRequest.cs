using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Models.Requests
{
    public class CatsRequest
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string? Name { get; set; }
    }
}
