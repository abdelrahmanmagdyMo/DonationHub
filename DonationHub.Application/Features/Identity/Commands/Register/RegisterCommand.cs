using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.Register
{
    public sealed record RegisterCommand(RegisterDto RegisterDto) : IRequest<Result<RegisterResponse>>;

}
