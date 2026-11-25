using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.Login
{
    public sealed record LoginCommand(LoginDto LoginDto)
        : IRequest<Result<LoginResponse>>;
}
