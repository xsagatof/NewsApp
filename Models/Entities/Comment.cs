using System;

namespace NewsApp.Models.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public User User { get; set; } = null!;
        public News News { get; set; } = null!;
    }
}