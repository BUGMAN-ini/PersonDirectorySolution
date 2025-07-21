using FluentValidation;
using FluentValidation.AspNetCore;
using PersonDirectory.API;
using PersonDirectory.API.Resources;
using PersonDirectory.Application;
using PersonDirectory.Application.Validators;
using PersonDirectory.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonDTOValidator>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResources));
    });

builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
