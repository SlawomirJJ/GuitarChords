using System.ComponentModel.DataAnnotations;

namespace GuitarChords.Dtos
{
    public class RegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$\"", ErrorMessage = "Minimum length 8 and must contain at least one number and includes both lower and uppercase letters and special characters")]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
        public string? Role { get; set; }
    }
}
