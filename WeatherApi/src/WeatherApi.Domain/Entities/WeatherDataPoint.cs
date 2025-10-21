using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherApi.Domain.Entities;

/// <summary>
/// Represents a weather data point for a specific city at a specific time.
/// </summary>
public class WeatherDataPoint
{
    /// <summary>
    /// Gets or sets the unique identifier for the weather data point.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    [BsonElement("city")]
    [BsonRequired]
    public required string City { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this weather data was recorded or forecasted for.
    /// </summary>
    [BsonElement("timestamp")]
    [BsonRequired]
    public required DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the temperature in Celsius.
    /// </summary>
    [BsonElement("temperature")]
    public double Temperature { get; set; }

    /// <summary>
    /// Gets or sets the humidity percentage (0-100).
    /// </summary>
    [BsonElement("humidity")]
    public double Humidity { get; set; }

    /// <summary>
    /// Gets or sets the wind speed in km/h.
    /// </summary>
    [BsonElement("windSpeed")]
    public double WindSpeed { get; set; }

    /// <summary>
    /// Gets or sets the weather condition description.
    /// </summary>
    [BsonElement("condition")]
    public string Condition { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when this data point was last updated.
    /// Used for upsert logic to determine if data is newer or older.
    /// </summary>
    [BsonElement("lastUpdated")]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

