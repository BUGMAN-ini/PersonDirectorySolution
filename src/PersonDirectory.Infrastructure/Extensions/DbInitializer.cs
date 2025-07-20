using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Infrastructure.Extensions
{
    public static class DbInitializer
    {
        public static async Task SeedInitialDataAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (await context.Cities.AnyAsync()) return;

            var cities = new List<City>
            {
                new City { Name = "Tbilisi", Country = "Georgia" },
                new City { Name = "Batumi", Country = "Georgia" },
                new City { Name = "Kutaisi", Country = "Georgia" },
                new City { Name = "Rustavi", Country = "Georgia" },
                new City { Name = "Gori", Country = "Georgia" }
            };

            await context.Cities.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }
    }
}
