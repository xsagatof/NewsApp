using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.DTOs.Comments
{
    public class CommentCreateDto
    {
        [Required(ErrorMessage = "Text is required")]
        [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "News ID is required")]
        public Guid NewsId { get; set; }
    }
}