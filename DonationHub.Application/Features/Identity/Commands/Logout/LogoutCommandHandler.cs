using DonationHub.Application.Contracts;
using DonationHub.Domain.Common.Results;
using DonationHub.Domain.Interfaces;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.Logout
{
    public sealed class LogoutCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, TimeProvider timeProvider) : IRequestHandler<LogoutCommand, Result>
    {
        public async Task<Result> Handle(LogoutCommand request, CancellationToken ct)
        {
            var hash = tokenService.Hash(request.RefreshToken);
            var token = await tokenService.GetHashAsync(hash, ct);
            if (token is { RevokedAt: null })
            {
                token.Revoke(timeProvider.GetUtcNow());
                await unitOfWork.SaveChangesAsync(ct);
            }
            return Result.Success();
        }
    }
}
