using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using WeatherApi.Api.Middleware;
using WeatherApi.Application.Commands;
using WeatherApi.Application.Queries;
using WeatherApi.Application.Validators;
using WeatherApi.Domain.Repositories;
using WeatherApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration["MongoDB:DatabaseName"] ?? "weatherdb";

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));
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

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add API key authentication middleware
app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

app.MapControllers();

app.Run();
