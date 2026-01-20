using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.DTOs.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}