
using DonationHub.Infrastructure.Extensions;
using DonationHub.Infrastructure.Persistence;
using DonationHub.Infrastructure.Persistence.Seeding;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

namespace DonationHub.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
                });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });
            builder.Services.InfrastructureRegisterServices(builder.Configuration, builder.Environment);
            builder.Services.AddOpenApi(o =>
            {
                o.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });

            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();

                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await CategorySeeding.SeedAsync(context);
                await app.Services.SeedAdminAsync();
                await app.Services.SeedDonorAsync();
                await app.Services.SeedManagerAsync();

            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
