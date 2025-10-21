namespace WeatherApi.Application.DTOs;

/// <summary>
/// Response DTO for weather data queries.
/// </summary>
public class WeatherDataResponse
{
    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Gets or sets the timestamp for this weather data.
    /// </summary>
    public required DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the temperature in Celsius.
    /// </summary>
    public double Temperature { get; set; }

    /// <summary>
    /// Gets or sets the humidity percentage.
    /// </summary>
    public double Humidity { get; set; }

    /// <summary>
    /// Gets or sets the wind speed in km/h.
    /// </summary>
    public double WindSpeed { get; set; }

    /// <summary>
    /// Gets or sets the weather condition.
    /// </summary>
    public string Condition { get; set; } = string.Empty;
}

