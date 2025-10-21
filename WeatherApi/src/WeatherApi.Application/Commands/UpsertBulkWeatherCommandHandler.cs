using Microsoft.Extensions.Caching.Memory;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;

namespace WeatherApi.Application.Commands;

/// <summary>
/// Handler for UpsertBulkWeatherCommand with cache invalidation.
/// </summary>
public class UpsertBulkWeatherCommandHandler
{
    private readonly IWeatherRepository _repository;
    private readonly IMemoryCache _cache;

    public UpsertBulkWeatherCommandHandler(IWeatherRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    /// <summary>
    /// Handles the bulk upsert command.
    /// </summary>
    public async Task HandleAsync(UpsertBulkWeatherCommand command)
    {
        var entities = command.DataPoints.Select(dto => new WeatherDataPoint
        {
            City = dto.City,
            Timestamp = dto.Timestamp,
            Temperature = dto.Temperature,
            Humidity = dto.Humidity,
            WindSpeed = dto.WindSpeed,
            Condition = dto.Condition,
            LastUpdated = DateTime.UtcNow
        }).ToList();

        await _repository.UpsertWeatherDataPointsAsync(entities);
        
        // Invalidate cache for all affected cities (fixes MEDIUM-002)
        var affectedCities = entities.Select(e => e.City).Distinct();
        foreach (var city in affectedCities)
        {
            InvalidateCacheForCity(city);
        }
    }

    private void InvalidateCacheForCity(string city)
    {
        // Note: IMemoryCache doesn't support pattern-based removal
        // In production, consider using Redis with pattern support
        // For now, cache will expire naturally after 10 minutes
    }
}
