using System.ComponentModel.DataAnnotations;

namespace GuitarChords.Models.Dtos.Requests
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
