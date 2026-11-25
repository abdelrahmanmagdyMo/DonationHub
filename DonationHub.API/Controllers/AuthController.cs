using DonationHub.Application.Features.Identity.Commands.Login;
using DonationHub.Application.Features.Identity.Commands.Logout;
using DonationHub.Application.Features.Identity.Commands.Refresh;
using DonationHub.Application.Features.Identity.Commands.Register;
using DonationHub.Application.Features.Identity.Commands.RegisterAdmin;
using DonationHub.Application.Features.Identity.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonationHub.API.Controllers
{
    public class AuthController(ISender sender) : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand, CancellationToken ct)
        => ToActionResult(await sender.Send(registerCommand, ct));


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand, CancellationToken ct)
       => ToActionResult(await sender.Send(loginCommand, ct));


        [AllowAnonymous]
        [HttpPost("logout")]

        public async Task<IActionResult> Logout(LogoutCommand logoutCommand, CancellationToken ct)
       => ToActionResult(await sender.Send(logoutCommand, ct));


        [HttpPost("me")]
        [Authorize]
        public async Task<IActionResult> GetMe(CancellationToken ct)
            => ToActionResult(await sender.Send(new GetCurrentUserQuery(), ct));


        [Authorize(Roles = "Admin")]
        [HttpPost("admin/staff")]
        public async Task<IActionResult> CreateStaff(RegisterAdminCommand adminCommand, CancellationToken ct)
      => ToActionResult(await sender.Send(adminCommand, ct));

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenCommand refreshToken, CancellationToken ct)
       => ToActionResult(await sender.Send(refreshToken, ct));

    }
}
