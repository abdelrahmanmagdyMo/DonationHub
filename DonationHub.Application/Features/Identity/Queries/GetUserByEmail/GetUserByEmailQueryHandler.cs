using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Application.IdentityExceptions;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Queries.GetUserByEmail
{
    public sealed class GetUserByEmailQueryHandler(IIdentityService identityService)
        : IRequestHandler<GetUserByEmailQuery, Result<GetUserByEmailResponse>>
    {
        public async Task<Result<GetUserByEmailResponse>> Handle(GetUserByEmailQuery request, CancellationToken ct)
        {
            var userResult = await identityService.GetUserByEmailAsync(request.Email, ct);
            if (userResult.IsFailure) return Result<GetUserByEmailResponse>.Failure(AuthErrors.InvalidCredentials);
            var user = userResult.Value;
            var response = new GetUserByEmailResponse(user.Id, user.FullName, user.Email, user.roles.FirstOrDefault()!);
            return Result<GetUserByEmailResponse>.Success(response);
        }
    }
}
