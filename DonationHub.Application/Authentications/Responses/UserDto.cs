namespace DonationHub.Application.Authentications.Responses
{
    public record UserDto(Guid Id, string Email, string FullName, bool IsActive, IEnumerable<string> roles);
}
