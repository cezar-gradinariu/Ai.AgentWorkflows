namespace WeatherApi.Api.Middleware;

/// <summary>
/// Middleware to validate API keys for protected endpoints.
/// Supports configuration via appsettings or environment variable WEATHER_API_KEY.
/// </summary>
public class ApiKeyAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-API-Key";
    private const string ApiKeyEnvironmentVariable = "WEATHER_API_KEY";
    private static readonly string[] PublicMethods = { HttpMethods.Get, HttpMethods.Options, HttpMethods.Head };

    public ApiKeyAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration, ILogger<ApiKeyAuthenticationMiddleware> logger)
    {
        // Skip authentication for public HTTP methods (GET, OPTIONS, HEAD)
        if (PublicMethods.Contains(context.Request.Method))
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

        // For other requests (POST, PUT, DELETE, etc.), require API key
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            logger.LogWarning("API request received without API key header");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { Message = "API Key is missing" });
            return;
        }

        // Check environment variable first, then fallback to configuration (fixes CRITICAL-002)
        var validApiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable) 
                         ?? configuration["ApiKey"];
        
        if (string.IsNullOrEmpty(validApiKey))
        {
            logger.LogError("API Key is not configured. Set WEATHER_API_KEY environment variable or ApiKey in configuration.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "API Key configuration error" });
            return;
        }

        if (extractedApiKey != validApiKey)
        {
            logger.LogWarning("Invalid API key attempt from {RemoteIp}", context.Connection.RemoteIpAddress);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { Message = "Invalid API Key" });
            return;
        }

        await _next(context);
    }
}
