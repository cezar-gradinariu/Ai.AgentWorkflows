using WeatherApi.Domain.Entities;

namespace WeatherApi.Domain.Repositories;

/// <summary>
/// Repository interface for weather data operations.
/// </summary>
public interface IWeatherRepository
{
    /// <summary>
    /// Gets weather data for a specific city within a date range.
    /// </summary>
    /// <param name="city">The city name.</param>
    /// <param name="from">Start date/time (inclusive).</param>
    /// <param name="to">End date/time (inclusive).</param>
    /// <returns>List of weather data points sorted by timestamp.</returns>
    Task<List<WeatherDataPoint>> GetWeatherAsync(string city, DateTime from, DateTime to);

    /// <summary>
    /// Upserts multiple weather data points in bulk.
    /// If data exists for the same city and timestamp, it will be updated only if newer.
    /// </summary>
    /// <param name="dataPoints">The weather data points to upsert.</param>
    Task UpsertWeatherDataPointsAsync(IEnumerable<WeatherDataPoint> dataPoints);

    /// <summary>
    /// Upserts a single weather data point.
    /// If data exists for the same city and timestamp, it will be updated only if newer.
    /// </summary>
    /// <param name="dataPoint">The weather data point to upsert.</param>
    Task UpsertWeatherDataPointAsync(WeatherDataPoint dataPoint);
}

