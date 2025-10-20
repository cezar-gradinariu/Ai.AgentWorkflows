using System.Net;
using System.Text.Json;
using WeatherApi.Application.DTOs.Responses;

namespace WeatherApi.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new ErrorResponse
        {
            Code = "INTERNAL_SERVER_ERROR",
            Message = exception.Message,
            Timestamp = DateTime.UtcNow
        };

        if (exception is UnauthorizedAccessException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            errorResponse.Code = "UNAUTHORIZED";
        }
        else if (exception is InvalidOperationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            errorResponse.Code = "BAD_REQUEST";
        }

        var response = ApiResponse<object>.FailureResponse(errorResponse);
        var json = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(json);
    }
}

