using System.Text.Json.Serialization;

namespace HomeWork_06._02._2026.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author? Author { get; set; }
    }
}