using HomeWork_09._02._2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_09._02._2026.Storage
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}