namespace DonationHub.Application.Authentications.Responses
{
    public sealed record GetUserByEmailResponse(Guid Id,
     string FullName,
     string Email,
     string Role);


}
