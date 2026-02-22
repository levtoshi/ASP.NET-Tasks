using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Models
{
    public class AnalyticsItem
    {
        public int Id { get; set; }
        public int CryptocurrencyId { get; set; }
        public bool Bull { get; set; }
        public decimal TargetPrice { get; set; }
        public byte Risk { get; set; }
    }
}