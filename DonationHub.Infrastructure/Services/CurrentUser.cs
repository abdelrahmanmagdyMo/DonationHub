using DonationHub.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DonationHub.Infrastructure.Services
{
    public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
    {
        private ClaimsPrincipal? User => accessor.HttpContext!.User;
        public Guid? UserId => Guid.TryParse(accessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        public bool IsAdmin => User?.IsInRole("Admin") ?? false;
    }
}
