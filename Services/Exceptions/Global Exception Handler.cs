using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, details) = MapException(exception);

        var result = new
        {
            title,
            details,
            statusCode
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(
            JsonSerializer.Serialize(result),
            cancellationToken
        );

        return true;
    }

    private static (int statusCode, string title, string details) MapException(Exception exception)
        => exception switch
        {
            NotFoundException ex =>
                (StatusCodes.Status404NotFound, "Not Found", ex.Message),

            BadRequestException ex =>
                (StatusCodes.Status400BadRequest, "Bad Request", ex.Message),

            UnauthorizedException ex =>
                (StatusCodes.Status401Unauthorized, "Unauthorized", ex.Message),

            _ =>
                (StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected server error occurred.")
        };
}
