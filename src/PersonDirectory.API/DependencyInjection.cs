using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return app;
        }
    }
}
