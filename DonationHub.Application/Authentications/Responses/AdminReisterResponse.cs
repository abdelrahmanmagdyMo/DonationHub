namespace DonationHub.Application.Authentications.Responses
{
    public class AdminRegisterResponse
    {
        public string Email { get; set; }
        public string TemporaryPassword { get; set; }
        public string Role { get; set; }
    }
}
