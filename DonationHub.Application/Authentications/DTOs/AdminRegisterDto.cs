using System.ComponentModel.DataAnnotations;

namespace DonationHub.Application.Authentications.DTOs
{
    public class AdminRegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; init; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string FullName { get; init; } = string.Empty;
    }
}
