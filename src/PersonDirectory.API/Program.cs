using FluentValidation;
using PersonDirectory.API;
using PersonDirectory.Application;
using PersonDirectory.Application.Validators;
using PersonDirectory.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonDTOValidator>();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


var supportedCultures = new[] { "en", "ka" };

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
