using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Helpers;
using NewsApp.Interfaces;
using NewsApp.Models.DTOs.Auth;
using NewsApp.Models.DTOs.User;
using NewsApp.Models.Entities;
using NewsApp.Models.Enums;

namespace NewsApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                // Validate phone number
                if (!PhoneNumberValidator.TryNormalizePhoneNumber(registerRequest.PhoneNumber, out string normalizedPhone))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid phone number format"
                    };
                }

                // Check if user already exists
                if (await UserExistsAsync(normalizedPhone))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "User with this phone number already exists"
                    };
                }

                // Create new user
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = normalizedPhone,
                    PasswordHash = PasswordHelper.HashPassword(registerRequest.Password),
                    FullName = registerRequest.FullName,
                    Role = UserRole.User,
                    CreatedAt = DateTime.UtcNow
                };

                // Save user to database
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // Generate token
                var userDto = new UserDto
                {
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt
                };

                var token = _tokenService.GenerateToken(userDto);

                return new AuthResponse
                {
                    Success = true,
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    User = userDto,
                    Message = "Registration successful"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                // Normalize phone number
                if (!PhoneNumberValidator.TryNormalizePhoneNumber(loginRequest.PhoneNumber, out string normalizedPhone))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid phone number format"
                    };
                }

                // Find user by phone number
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == normalizedPhone);

                if (user == null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                // Verify password
                if (!PasswordHelper.VerifyPassword(loginRequest.Password, user.PasswordHash))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid credentials"
                    };
                }

                // Generate token
                var userDto = new UserDto
                {
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt
                };

                var token = _tokenService.GenerateToken(userDto);

                return new AuthResponse
                {
                    Success = true,
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    User = userDto,
                    Message = "Login successful"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Login failed: {ex.Message}"
                };
            }
        }

        public async Task<bool> UserExistsAsync(string phoneNumber)
        {
            if (!PhoneNumberValidator.TryNormalizePhoneNumber(phoneNumber, out string normalizedPhone))
                return false;

            return await _context.Users
                .AnyAsync(u => u.PhoneNumber == normalizedPhone);
        }
    }
}