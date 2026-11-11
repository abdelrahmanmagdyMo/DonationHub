using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Features.Auth;
using DonationHub.Infrastructure.Persistence;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DonationHub.API.Controllers
{
    public class AuthController(AppDbContext context, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IAuthService authService) : BaseApiController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            await authService.RegisterAsync(register);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await authService.LoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(RefreshTokenRequestDto dto)
        {
            await authService.RevokeRefreshTokenAsync(dto);
            return NoContent();
        }
        [HttpPost("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdValue, out var userId)) return Unauthorized();
            var user = await authService.GetUserByIdAsync(userId.ToString());
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("admin/staff")]
        public async Task<IActionResult> CreateStaff([FromBody] AdminRegisterDto dto)
        {
            await authService.RegisterAdminORCampignManager(dto);
            return Ok();
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
        {
            var result = await authService.RefreshTokenAsync(dto);
            return Ok(result);
        }

    }
}
