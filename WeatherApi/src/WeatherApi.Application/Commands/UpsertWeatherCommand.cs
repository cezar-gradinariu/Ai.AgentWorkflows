using WeatherApi.Application.DTOs;

namespace WeatherApi.Application.Commands;

/// <summary>
/// Command to upsert a single weather data point.
/// </summary>
public class UpsertWeatherCommand
{
    /// <summary>
    /// Gets or sets the weather data point to upsert.
    /// </summary>
    public required WeatherDataRequest DataPoint { get; set; }
}

