using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.RegisterAdmin
{
    public sealed record RegisterAdminCommand(AdminRegisterDto RegisterDto) : IRequest<Result<AdminRegisterResponse>>;

}
