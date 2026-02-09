using System.Security.Cryptography.Pkcs;
using System.Text.Json.Serialization;

namespace HomeWork_09._02._2026.Models
{

    public enum SubscriptionType
    {
        Free,
        Standard,
        Premium
    }

    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public SubscriptionType Type { get; set; }

        public int UserId { get; set; }
    }
}