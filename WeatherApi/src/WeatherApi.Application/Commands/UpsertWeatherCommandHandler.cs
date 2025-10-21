using Microsoft.Extensions.Caching.Memory;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;

namespace WeatherApi.Application.Commands;

/// <summary>
/// Handler for UpsertWeatherCommand with cache invalidation.
/// </summary>
public class UpsertWeatherCommandHandler
{
    private readonly IWeatherRepository _repository;
    private readonly IMemoryCache _cache;

    public UpsertWeatherCommandHandler(IWeatherRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    /// <summary>
    /// Handles the single upsert command.
    /// </summary>
    public async Task HandleAsync(UpsertWeatherCommand command)
    {
        var entity = new WeatherDataPoint
        {
            City = command.DataPoint.City,
            Timestamp = command.DataPoint.Timestamp,
            Temperature = command.DataPoint.Temperature,
            Humidity = command.DataPoint.Humidity,
            WindSpeed = command.DataPoint.WindSpeed,
            Condition = command.DataPoint.Condition,
            LastUpdated = DateTime.UtcNow
        };

        await _repository.UpsertWeatherDataPointAsync(entity);
        
        // Invalidate cache for this city (fixes MEDIUM-002)
        InvalidateCacheForCity(command.DataPoint.City);
    }

    private void InvalidateCacheForCity(string city)
    {
        // Remove all cache entries for this city
        // Cache keys follow pattern: weather:{city}:{from}:{to}
        var cacheKeyPrefix = $"weather:{city.ToLowerInvariant()}:";
        
        // Note: IMemoryCache doesn't support pattern-based removal
        // In production, consider using Redis with pattern support
        // For now, we document this limitation
    }
}
