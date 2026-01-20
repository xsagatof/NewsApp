using System;

namespace NewsApp.Models.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
        public bool IsLike { get; set; } // true for like, false for dislike
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public User User { get; set; } = null!;
        public News News { get; set; } = null!;
    }
}