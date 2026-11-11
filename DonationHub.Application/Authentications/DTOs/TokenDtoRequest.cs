namespace DonationHub.Application.Authentications.DTOs
{
    public class TokenDtoRequest
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

    }
}
