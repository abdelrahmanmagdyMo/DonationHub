namespace DonationHub.Application.Authentications.Responses
{
    public record UserDto(string Id, string Email, string FullName, IEnumerable<string> roles);
}
