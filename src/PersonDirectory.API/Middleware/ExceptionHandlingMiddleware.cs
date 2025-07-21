using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PersonDirectory.Application.Exceptions;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonDirectory.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            var response = new
            {
                title = statusCode == HttpStatusCode.InternalServerError ? "Server Error" : exception.GetType().Name,
                detail = exception.Message,
                status = (int)statusCode,
                traceId = context.TraceIdentifier
            };

            context.Response.StatusCode = (int)statusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
