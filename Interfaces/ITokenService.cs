using NewsApp.Models.DTOs.User;
using NewsApp.Models.Entities;

namespace NewsApp.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateToken(UserDto userDto);
        Guid? ValidateToken(string token);
    }
}