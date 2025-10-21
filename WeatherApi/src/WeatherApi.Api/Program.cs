using AspNetCoreRateLimit;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Serilog;
using WeatherApi.Api.Middleware;
using WeatherApi.Application.Commands;
using WeatherApi.Application.Queries;
using WeatherApi.Application.Validators;
using WeatherApi.Domain.Repositories;
using WeatherApi.Infrastructure.Repositories;

// Configure Serilog (fixes HIGH-004)
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/weatherapi-.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Starting Weather API application");

    var builder = WebApplication.CreateBuilder(args);

    // Add Serilog
    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    
    // Configure Swagger with API Key authentication (fixes MEDIUM-005)
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "Weather API", 
            Version = "v1",
            Description = "Weather data API with public GET and authenticated POST endpoints"
        });
        
        c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Description = "API Key authentication. Header: X-API-Key",
            In = ParameterLocation.Header,
            Name = "X-API-Key",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "ApiKeyScheme"
        });
        
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    // Validate MongoDB connection string (fixes MEDIUM-003)
    var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB");
    if (string.IsNullOrEmpty(mongoConnectionString))
    {
        throw new InvalidOperationException(
            "MongoDB connection string is required. Configure 'ConnectionStrings:MongoDB' in appsettings.json or environment variable.");
    }
    
    var mongoDatabaseName = builder.Configuration["MongoDB:DatabaseName"];
    if (string.IsNullOrEmpty(mongoDatabaseName))
    {
        throw new InvalidOperationException(
            "MongoDB database name is required. Configure 'MongoDB:DatabaseName' in appsettings.json.");
    }

    // Configure MongoDB
    builder.Services.AddSingleton<IMongoClient>(sp => 
    {
        var logger = sp.GetRequiredService<ILogger<Program>>();
        try
        {
            // Use MongoClientSettings for more robust configuration
            var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
            settings.ConnectTimeout = TimeSpan.FromSeconds(10);
            
            var client = new MongoClient(settings);
            // Test connection
            client.GetDatabase(mongoDatabaseName).RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}").Wait();
            logger.LogInformation("Successfully connected to MongoDB at {ConnectionString}", mongoConnectionString);
            return client;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to connect to MongoDB at {ConnectionString}", mongoConnectionString);
            throw;
        }
    });
    
    builder.Services.AddScoped(sp =>
    {
        var client = sp.GetRequiredService<IMongoClient>();
        return client.GetDatabase(mongoDatabaseName);
    });

    // Register repositories
    builder.Services.AddScoped<IWeatherRepository, MongoWeatherRepository>();

    // Register CQRS handlers
    builder.Services.AddScoped<GetWeatherQueryHandler>();
    builder.Services.AddScoped<UpsertWeatherCommandHandler>();
    builder.Services.AddScoped<UpsertBulkWeatherCommandHandler>();

    // Register validators
    builder.Services.AddScoped<WeatherDataRequestValidator>();

    // Add in-memory caching
    builder.Services.AddMemoryCache();

    // Configure rate limiting (fixes CRITICAL-003)
    builder.Services.Configure<IpRateLimitOptions>(options =>
    {
        options.EnableEndpointRateLimiting = true;
        options.StackBlockedRequests = false;
        options.HttpStatusCode = 429;
        options.RealIpHeader = "X-Real-IP";
        options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "GET:/api/weather/*",
                Period = "1m",
                Limit = 100
            },
            new RateLimitRule
            {
                Endpoint = "POST:/api/weather",
                Period = "1m",
                Limit = 10
            },
            new RateLimitRule
            {
                Endpoint = "POST:/api/weather/bulk",
                Period = "1m",
                Limit = 5
            }
        };
    });

    builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

    // Add health checks (fixes HIGH-002)
    builder.Services.AddHealthChecks()
        .AddMongoDb(
            sp => sp.GetRequiredService<IMongoClient>(),
            name: "mongodb", 
            failureStatus: HealthStatus.Unhealthy,
            tags: new[] { "db", "mongodb" });

    var app = builder.Build();

    // Configure the HTTP request pipeline
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });

    app.UseHttpsRedirection();

    // Use Serilog request logging
    app.UseSerilogRequestLogging();

    // Add rate limiting middleware
    app.UseIpRateLimiting();

    // Add API key authentication middleware
    app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

    // Map health check endpoint
    app.MapHealthChecks("/health");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
