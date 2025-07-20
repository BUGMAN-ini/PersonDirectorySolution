using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.API;
using PersonDirectory.API.Middleware;
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

// global exception handler → ProblemDetails JSON
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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
