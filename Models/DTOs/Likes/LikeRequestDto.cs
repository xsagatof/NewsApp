namespace NewsApp.Models.DTOs.Likes
{
    public class LikeRequestDto
    {
        public Guid NewsId { get; set; }
        public bool IsLike { get; set; } // true for like, false for dislike
    }
}