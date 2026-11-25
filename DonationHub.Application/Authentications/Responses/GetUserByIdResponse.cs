namespace DonationHub.Application.Authentications.Responses
{
    public record GetUserByIdResponse
    (
        Guid Id,
     string FullName,
     string Email,
     string Role
 );
}