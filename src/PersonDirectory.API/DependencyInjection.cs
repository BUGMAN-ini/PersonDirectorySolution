using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PersonDirectory.API.Middleware;
using PersonDirectory.Infrastructure;

namespace PersonDirectory.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"));
            services.AddLocalization(o => o.ResourcesPath = "Resources");
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.Services.ApplyMigrations();

            app.UseRequestLocalization(opt =>
            {
                var cultures = new[] { "en-US", "ka-GE" };
                opt.SetDefaultCulture("en-US")
                   .AddSupportedCultures(cultures)
                   .AddSupportedUICultures(cultures);
            });

            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            app.UseStaticFiles();
            app.UseMiddleware<LocalizationMiddleware>();
            app.UseExceptionHandlingMiddleware();
            app.UseLocalizationMiddleware();
            app.UseExceptionHandler("/error");

            return app;
        }
    }
}
