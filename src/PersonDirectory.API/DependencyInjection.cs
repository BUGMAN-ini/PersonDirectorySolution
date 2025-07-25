using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.API.Middleware;
using PersonDirectory.API.Resources;
using PersonDirectory.Application.Validators;
using PersonDirectory.Infrastructure;

namespace PersonDirectory.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!);

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.PostConfigure<MvcOptions>(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "ველის შევსება სავალდებულოა.");
            });

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

            app.UseExceptionHandler(c => c.Run(async ctx =>
            {
                var err = ctx.Features.Get<IExceptionHandlerPathFeature>()?.Error;
                ctx.Response.StatusCode = 500;
                await ctx.Response.WriteAsJsonAsync(new
                {
                    title = "Server error",
                    detail = err?.Message
                });
            }));

            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            app.UseStaticFiles();
            app.UseExceptionHandlingMiddleware();
            app.UseLocalizationMiddleware();
            app.UseExceptionHandler("/error");

            return app;
        }
    }
}
