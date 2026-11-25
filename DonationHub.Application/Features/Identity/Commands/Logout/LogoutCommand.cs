using DonationHub.Domain.Common.Results;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.Logout
{
    public sealed record LogoutCommand(string RefreshToken) : IRequest<Result>;

}
