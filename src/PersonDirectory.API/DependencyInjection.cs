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

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.Services.ApplyMigrations();

            app.UseRequestLocalization();
            
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            app.UseExceptionHandlingMiddleware();
            app.UseLocalizationMiddleware();

            return app;
        }
    }
}
