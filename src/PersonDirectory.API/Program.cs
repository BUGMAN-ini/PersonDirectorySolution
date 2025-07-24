using FluentValidation;
using FluentValidation.AspNetCore;
using PersonDirectory.API;
using PersonDirectory.API.Middleware;
using PersonDirectory.API.Resources;
using PersonDirectory.Application;
using PersonDirectory.Application.Validators;
using PersonDirectory.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseLocalizationMiddleware();

app.UseApiServices();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
