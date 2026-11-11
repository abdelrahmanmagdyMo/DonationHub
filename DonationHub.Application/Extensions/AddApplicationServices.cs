using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DonationHub.Application.Extensions
{
    public static class AddApplicationServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {

            return services;
        }
    }
}
