using HomeWork_06._02._2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_06._02._2026.Storage
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}