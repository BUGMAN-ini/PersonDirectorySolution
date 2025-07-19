using PersonDirectory.API;
using PersonDirectory.Application;
using PersonDirectory.Application.Interfaces.Repositories;
using PersonDirectory.Infrastructure;
using PersonDirectory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseApiServices();

app.Run();
