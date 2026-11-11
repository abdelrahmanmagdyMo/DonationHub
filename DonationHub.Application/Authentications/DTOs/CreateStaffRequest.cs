namespace DonationHub.Application.Authentications.DTOs
{
    public sealed record CreateStaffRequest
    {
        public string Email { get; init; }
        public string FullName { get; init; }
        public string Role { get; init; }
    }
}
