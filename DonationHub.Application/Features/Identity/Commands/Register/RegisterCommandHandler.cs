using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;
namespace DonationHub.Application.Features.Identity.Commands.Register
{
    public sealed class RegisterCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
    {
        public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken ct)
        {
            var userResult = await identityService.CreateUserAsync(request.RegisterDto, ct);
            if (userResult.IsFailure) return Result<RegisterResponse>.Failure(userResult.Error);

            return Result<RegisterResponse>.Success(new RegisterResponse(request.RegisterDto.Email, "New User Created Successfully"));

        }
    }
}
