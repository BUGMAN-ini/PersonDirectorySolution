using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Application.Interfaces.Services;
using PersonDirectory.Application.Services;
using PersonDirectory.Infrastructure.Repositories;

namespace PersonDirectory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IRelatedPersonRepository, RelatedPersonRepository>();
            services.AddScoped<ICityRepository,CityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
