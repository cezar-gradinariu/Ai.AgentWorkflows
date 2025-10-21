using WeatherApi.Application.DTOs;

namespace WeatherApi.Application.Commands;

/// <summary>
/// Command to upsert multiple weather data points in bulk.
/// </summary>
public class UpsertBulkWeatherCommand
{
    /// <summary>
    /// Gets or sets the list of weather data points to upsert.
    /// </summary>
    public required List<WeatherDataRequest> DataPoints { get; set; }
}

