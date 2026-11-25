using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Application.IdentityExceptions;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;
namespace DonationHub.Application.Features.Identity.Queries.GetCurrentUser
{
    public sealed class GetCurrentUserQueryHandler(ICurrentUser currentUser, IIdentityService identityService) : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken ct)
        {
            var idResult = currentUser.UserId;
            if (idResult is null) return Result<UserDto>.Failure(AuthErrors.InvalidCredentials);
            var userId = idResult.Value;
            return await identityService.GetUserByIdAsync(userId, ct);
        }
    }
}
