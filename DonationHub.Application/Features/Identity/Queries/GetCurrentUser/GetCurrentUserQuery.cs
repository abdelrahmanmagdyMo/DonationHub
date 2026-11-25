using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Queries.GetCurrentUser
{
    public sealed record GetCurrentUserQuery : IRequest<Result<UserDto>>;

}
