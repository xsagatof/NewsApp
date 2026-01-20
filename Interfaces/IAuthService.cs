using NewsApp.Models.DTOs.Auth;

namespace NewsApp.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
        Task<bool> UserExistsAsync(string phoneNumber);
    }
}