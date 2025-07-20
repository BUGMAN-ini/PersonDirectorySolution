using Microsoft.EntityFrameworkCore;
using PersonDirectory.API;
using PersonDirectory.Application;
using PersonDirectory.Infrastructure;
using PersonDirectory.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.ApplyMigrations();

app.UseHttpsRedirection();

app.UseApiServices();

app.UseAuthorization();

app.MapControllers();

app.Run();
