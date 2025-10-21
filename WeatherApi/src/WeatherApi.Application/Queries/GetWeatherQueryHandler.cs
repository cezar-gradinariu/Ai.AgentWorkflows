using Microsoft.Extensions.Caching.Memory;
using WeatherApi.Application.DTOs;
using WeatherApi.Domain.Repositories;

namespace WeatherApi.Application.Queries;

/// <summary>
/// Handler for GetWeatherQuery with caching support.
/// </summary>
public class GetWeatherQueryHandler
{
    private readonly IWeatherRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public GetWeatherQueryHandler(IWeatherRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    /// <summary>
    /// Handles the weather query with caching.
    /// </summary>
    public async Task<List<WeatherDataResponse>> HandleAsync(GetWeatherQuery query)
    {
        // Ensure days is between 1 and 5
        var days = Math.Clamp(query.Days, 1, 5);
        var from = DateTime.UtcNow;
        var to = from.AddDays(days);
        
        var cacheKey = $"weather:{query.City.ToLowerInvariant()}:{from:yyyyMMddHH}:{to:yyyyMMddHH}";

        if (_cache.TryGetValue<List<WeatherDataResponse>>(cacheKey, out var cachedData) && cachedData != null)
        {
            return cachedData;
        }

        var weatherData = await _repository.GetWeatherAsync(query.City, from, to);
        
        var response = weatherData.Select(w => new WeatherDataResponse
        {
            City = w.City,
            Timestamp = w.Timestamp,
            Temperature = w.Temperature,
            Humidity = w.Humidity,
            WindSpeed = w.WindSpeed,
            Condition = w.Condition
        }).ToList();

        // Add defensive check before caching (fixes Bug #1)
        if (response != null && response.Any())
        {
            _cache.Set(cacheKey, response, _cacheDuration);
        }
        
        return response ?? new List<WeatherDataResponse>();
    }
}
