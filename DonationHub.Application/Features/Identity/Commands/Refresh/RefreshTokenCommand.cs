using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.Refresh
{
    public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<LoginResponse>>;


}
