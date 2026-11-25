using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Commands.RegisterAdmin
{
    public sealed class RegisterAdminCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterAdminCommand, Result<AdminRegisterResponse>>
    {
        public async Task<Result<AdminRegisterResponse>> Handle(RegisterAdminCommand request, CancellationToken ct)
        => await identityService.CreateStaffUserAsync(request.RegisterDto.FullName, request.RegisterDto.Email
                , request.RegisterDto.Role, ct);
    }
}
