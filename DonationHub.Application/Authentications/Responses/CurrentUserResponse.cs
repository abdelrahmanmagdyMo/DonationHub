namespace DonationHub.Application.Authentications.Responses
{
    public class CurrentUserResponse
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public string FullName { get; init; }

        public List<string> Roles { get; init; }
    }
}
