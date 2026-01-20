using NewsApp.Models.DTOs.User;

namespace NewsApp.Models.DTOs.Auth
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public UserDto? User { get; set; }
        public string? Message { get; set; }
    }
}