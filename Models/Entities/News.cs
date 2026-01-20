using System;
using System.Collections.Generic;

namespace NewsApp.Models.Entities
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsPublished { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        
        // Navigation properties
        public User Author { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        
        // Computed property for like count
        public int LikeCount => Likes?.Count(l => l.IsLike) ?? 0;
        public int DislikeCount => Likes?.Count(l => !l.IsLike) ?? 0;
    }
}