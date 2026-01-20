using Microsoft.EntityFrameworkCore;
using NewsApp.Models.Entities;

namespace NewsApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // Phone number must be unique
                entity.HasIndex(e => e.PhoneNumber)
                    .IsUnique();
                
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
                
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.FullName)
                    .HasMaxLength(100);
                
                entity.Property(e => e.Role)
                    .IsRequired();
                
                // Relationships
                entity.HasMany(e => e.NewsArticles)
                    .WithOne(e => e.Author)
                    .HasForeignKey(e => e.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasMany(e => e.Comments)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasMany(e => e.Likes)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure News entity
            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Content)
                    .IsRequired();
                
                entity.Property(e => e.Category)
                    .HasMaxLength(50);
                
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500);
                
                // Relationships
                entity.HasMany(e => e.Comments)
                    .WithOne(e => e.News)
                    .HasForeignKey(e => e.NewsId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasMany(e => e.Likes)
                    .WithOne(e => e.News)
                    .HasForeignKey(e => e.NewsId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Comment entity
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            // Configure Like entity
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // One user can only like/dislike a news article once
                entity.HasIndex(e => new { e.UserId, e.NewsId })
                    .IsUnique();
                
                entity.Property(e => e.IsLike)
                    .IsRequired();
            });
        }
    }
}