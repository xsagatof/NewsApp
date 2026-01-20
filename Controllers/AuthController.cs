using Microsoft.AspNetCore.Mvc;
using NewsApp.Interfaces;
using NewsApp.Models.DTOs.Auth;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(registerRequest);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new
            {
                message = result.Message,
                token = result.Token,
                expiresAt = result.ExpiresAt,
                user = result.User
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(loginRequest);

            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new
            {
                message = result.Message,
                token = result.Token,
                expiresAt = result.ExpiresAt,
                user = result.User
            });
        }

        [HttpGet("check-phone/{phoneNumber}")]
        public async Task<IActionResult> CheckPhoneNumber(string phoneNumber)
        {
            var exists = await _authService.UserExistsAsync(phoneNumber);
            
            return Ok(new 
            { 
                exists,
                message = exists ? "Phone number is already registered" : "Phone number is available"
            });
        }
    }
}