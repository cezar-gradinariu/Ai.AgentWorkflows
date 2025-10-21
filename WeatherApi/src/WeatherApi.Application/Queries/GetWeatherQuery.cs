namespace WeatherApi.Application.Queries;

/// <summary>
/// Query to get weather data for a specific city.
/// </summary>
public class GetWeatherQuery
{
    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Gets or sets the number of days to retrieve (1-5).
    /// </summary>
    public int Days { get; set; } = 5;
}
