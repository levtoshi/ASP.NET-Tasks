namespace HomeWork_06._03._2026.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; }
        public string ImageUrl { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRw1VvHQ7ZzfHWdBODYTF64oFMKI_fIVQ-sJg&s";
    }
}