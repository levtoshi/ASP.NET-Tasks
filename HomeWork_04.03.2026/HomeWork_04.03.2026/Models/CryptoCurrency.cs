namespace HomeWork_04._03._2026.Models
{
    public class CryptoCurrency
    {
        public int Rank { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Change1h { get; set; }
        public double Change24h { get; set; }
        public double Change7d { get; set; }
        public long MarketCap { get; set; }
        public long Volume24h { get; set; }
        public string CirculatingSupply { get; set; } = string.Empty;
    }
}