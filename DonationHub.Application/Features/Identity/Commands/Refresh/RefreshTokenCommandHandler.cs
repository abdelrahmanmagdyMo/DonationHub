using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Identity.Errors;
using DonationHub.Domain.Identity.RefreshToken;
using DonationHub.Domain.Interfaces;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.Refresh
{
    public sealed class RefreshTokenCommandHandler(ITokenService tokenService, IIdentityService identityService,
       IUnitOfWork unitOfWork, TimeProvider timeProvider) : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            var now = timeProvider.GetUtcNow();
            var hash = tokenService.Hash(request.RefreshToken);
            var old = await tokenService.GetHashAsync(hash, ct);
            if (old is null || !old.IsActive(now))
                return Result<LoginResponse>.Failure(AuthErrors.RefreshTokenInvalid);

            var userResult = await identityService.GetUserByIdAsync(old.UserId, ct);
            if (userResult.IsFailure)
                return Result<LoginResponse>.Failure(AuthErrors.RefreshTokenInvalid);
            var user = userResult.Value;

            if (!user.IsActive)
                return Result<LoginResponse>.Failure(AuthErrors.AccountDeactivated);
            var newRefresh = tokenService.GenerateRefreshToken();
            var tokenHash = tokenService.Hash(newRefresh);
            var newRefreshToken = RefreshToken.Create(user.Id, tokenHash, timeProvider, tokenService.GetRefreshTokenExpiry(now));
            if (newRefreshToken.IsFailure) return Result<LoginResponse>.Failure(newRefreshToken.Error);
            old.Revoke(now, newRefreshToken.Value.Id);
            tokenService.AddRefreshToken(newRefreshToken.Value);
            await unitOfWork.SaveChangesAsync(ct);
            var access = tokenService.GenerateAccessToken(new Authentications.DTOs.TokenDtoRequest
            {
                Email = user.Email
            ,
                FullName = user.FullName,
                UserId = user.Id.ToString(),
                Roles = user.roles
            });
            return Result<LoginResponse>.Success(new LoginResponse(access, newRefresh));
        }
    }
}
