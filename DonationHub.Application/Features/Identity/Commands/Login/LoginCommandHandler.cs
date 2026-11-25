using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;
using Errors = DonationHub.Domain.Identity.Errors;
namespace DonationHub.Application.Features.Identity.Commands.Login
{
    public sealed class LoginCommandHandler
        (IIdentityService identityService, ITokenService tokenService) : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken ct)
        {

            var userResult = await identityService.GetUserByEmailAsync(request.LoginDto.Email, ct);
            if (userResult.IsFailure) return Result<LoginResponse>.Failure(Errors.AuthErrors.InvalidCredentials);
            var user = userResult.Value;
            var isPasswordValid = await identityService.CheckPasswordAsync(request.LoginDto.Email, request.LoginDto.Password, ct);
            if (!isPasswordValid) return Result<LoginResponse>.Failure(Errors.AuthErrors.InvalidCredentials);
            var roleResult = await identityService.GetRolesAsync(user.Id, ct);
            if (roleResult.IsFailure) return Result<LoginResponse>.Failure(Errors.AuthErrors.InvalidCredentials);
            var tokenDto = new TokenDtoRequest
            {
                Email = request.LoginDto.Email,
                FullName = user.FullName,
                Roles = roleResult.Value,
                UserId = user.Id.ToString()
            };
            var accessToken = tokenService.GenerateAccessToken(tokenDto);
            var refreshToken = tokenService.GenerateRefreshToken();
            return Result<LoginResponse>.Success(new LoginResponse(

                AccessToken: accessToken,
                RefreshToken: refreshToken
            ));
        }
    }
}
