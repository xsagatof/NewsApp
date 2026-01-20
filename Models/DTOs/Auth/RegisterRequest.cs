using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [MinLength(10, ErrorMessage = "Phone number must be at least 10 digits")]
        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string? FullName { get; set; }
    }
}