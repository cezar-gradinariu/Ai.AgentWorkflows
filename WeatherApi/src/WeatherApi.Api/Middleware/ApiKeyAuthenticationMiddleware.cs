namespace WeatherApi.Api.Middleware;

/// <summary>
/// Middleware to validate API keys for protected endpoints.
/// </summary>
public class ApiKeyAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-API-Key";

    public ApiKeyAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        // Skip authentication for GET requests (public endpoint)
        if (context.Request.Method == HttpMethods.Get)
        {
            await _next(context);
            return;
        }

        // Skip authentication for Swagger endpoints
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        // For POST requests, require API key
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { Message = "API Key is missing" });
            return;
        }

        var validApiKey = configuration["ApiKey"];
        if (string.IsNullOrEmpty(validApiKey) || extractedApiKey != validApiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { Message = "Invalid API Key" });
            return;
        }

        await _next(context);
    }
}

