using System.Net;
using System.Text.Json;
using Serilog;
using FluentValidation;


namespace DigitalAssetsApp.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // LOG REAL
            Log.Error(ex, "Unhandled exception occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        // Validation errors
        if (ex is ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = validationException.Errors
                .Select(e => new
                {
                    field = e.PropertyName,
                    error = e.ErrorMessage
                });

            var result = JsonSerializer.Serialize(new
            {
                status = 400,
                errors
            });

            await context.Response.WriteAsync(result);
            return;
        }

        // Business errors
        if (ex.Message.Contains("Insufficient balance"))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new
            {
                status = 400,
                error = ex.Message
            });

            await context.Response.WriteAsync(result);
            return;
        }

        // General errors
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            status = 500,
            error = "An unexpected error occurred"
        };

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}