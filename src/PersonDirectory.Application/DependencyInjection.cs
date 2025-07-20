namespace PersonDirectory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICityService, CityService>();

            return services;
        }
    }
}
