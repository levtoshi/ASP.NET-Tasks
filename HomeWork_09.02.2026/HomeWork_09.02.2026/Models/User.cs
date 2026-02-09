namespace HomeWork_09._02._2026.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthday { get; set; }
        public string Email { get; set; }

        public List<Subscription> Subscriptions { get; set; } = new();
    }
}