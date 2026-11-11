using DonationHub.Application.Contracts;
using DonationHub.Application.Features.Auth;
using DonationHub.Infrastructure.Concrete;
using DonationHub.Infrastructure.Interceptor;
using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence;
using DonationHub.Infrastructure.Persistence.Identity;
using DonationHub.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace DonationHub.Infrastructure.Extensions
{
    public static class AddInfrastructureServices
    {
        public static IServiceCollection InfrastructureRegisterServices(this IServiceCollection services,
            IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IIdempotencyRecordRepository, IdempotencyRecordRepository>();
            services.AddScoped<IPaymentAttemptRepository, PaymentAttemptRepository>();
            services.AddScoped<IProcessedStripeEventRepository, ProcessedStripeEventRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("constr"))
                .AddInterceptors(new SoftDeleteInterceptor());
            }).AddIdentity<ApplicationUser, IdentityRole<Guid>>(op =>
            {
                //password config
                op.Password.RequireDigit = true;
                op.Password.RequiredLength = 12;
                op.Password.RequireLowercase = true;
                op.Password.RequireUppercase = true;
                // User config
                op.User.RequireUniqueEmail = true;

                // Lockout Config
                op.Lockout.AllowedForNewUsers = true;
                op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                op.Lockout.MaxFailedAccessAttempts = 5;

            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped(typeof(IDeletableRepository<,>), typeof(GenericRepository<,>));


            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<JwtOptions>>((o, jwtOptions) =>
                {
                    var jwt = jwtOptions.Value;
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
            services.AddOptions<JwtOptions>()
.BindConfiguration(JwtOptions.SectionName)
.ValidateDataAnnotations()
.ValidateOnStart();

            services.AddOptions<DonationOptions>()
                .BindConfiguration(DonationOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton(TimeProvider.System);
            if (environment.IsDevelopment())
            {

                services.AddOptions<SeedAdminOptions>()
                    .BindConfiguration(SeedAdminOptions.SectionName)
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

                services.AddOptions<CampaignManagerSeedOptions>()
                    .BindConfiguration(CampaignManagerSeedOptions.SectionName)
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

                services.AddOptions<SeedDonorOptions>()
                    .BindConfiguration(SeedDonorOptions.SectionName)
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
            }
            return services;
        }
    }
}
