
using DonationHub.Domain.Persistence.Identity;

namespace DonationHub.Application.Authentications.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public PrefferedLanguage PrefferedLanguage { get; set; } = PrefferedLanguage.EN;
    }
}
