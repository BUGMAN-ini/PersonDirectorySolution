using Carter;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Interfaces.Services;

namespace PersonDirectory.API.Endpoints
{
    public class CreateClient : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/persons/create", async (
                [FromForm] CreatePersonDTO request,
                IPersonService personService) =>
            {
                if (request is null)
                    return Results.BadRequest("Request body is required.");

                var id = await personService.CreatePersonAsync(request);

                return Results.Created($"/persons/{id}", new { Id = id });
            })
              .WithName("CreateOrder")
              .Produces<CreatePersonDTO>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Create Order")
              .WithDescription("Create Order");
        }
    }
}
