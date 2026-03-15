using TarasMessanger.Core.Entities;
using TarasMessanger.Core.Entities.Message;
using TarasMessanger.Core.Entities.Posts;
using TarasMessanger.Storage.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasMessanger.Storage
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<MessageBase> Messages { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostPhoto> PostPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TextMessage>()
                .HasBaseType<MessageBase>();

            builder.Entity<FileMessage>()
                .HasBaseType<MessageBase>();

            builder.Entity<GeoMessage>()
                .HasBaseType<MessageBase>();

            builder.Entity<Post>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.Text).HasMaxLength(2048);
                entity.HasMany(x => x.Photos)
                    .WithOne()
                    .HasForeignKey(x => x.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<PostPhoto>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Path).HasMaxLength(512).IsRequired();
            });

            base.OnModelCreating(builder);
        }
    }
}
