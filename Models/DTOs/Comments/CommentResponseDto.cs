namespace NewsApp.Models.DTOs.Comments
{
    public class CommentResponseDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid NewsId { get; set; }
        public string NewsTitle { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}