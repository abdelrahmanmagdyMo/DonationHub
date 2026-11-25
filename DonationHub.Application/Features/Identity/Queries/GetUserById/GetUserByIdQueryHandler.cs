using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Application.IdentityExceptions;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Queries.GetUserById
{
    public sealed class GetUserByIdQueryHandler(IIdentityService identityService) : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResponse>>
    {
        public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var userResult = await identityService.GetUserByIdAsync(request.UserId, ct);
            if (userResult is null) return Result<GetUserByIdResponse>.Failure(AuthErrors.InvalidCredentials);
            var user = userResult.Value;
            var response = new GetUserByIdResponse(request.UserId, user.FullName, user.Email, user.roles.FirstOrDefault()!);
            return Result<GetUserByIdResponse>.Success(response);
        }
    }
}
