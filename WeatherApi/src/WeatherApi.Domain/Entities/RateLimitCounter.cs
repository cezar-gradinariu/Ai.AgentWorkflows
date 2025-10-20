using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Domain.Entities;

public class RateLimitCounter : IAggregateRoot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }

    [BsonElement("ipAddress")]
    public string? IpAddress { get; set; }

    [BsonElement("endpoint")]
    public string Endpoint { get; set; } = string.Empty;

    [BsonElement("requestCount")]
    public int RequestCount { get; set; }

    [BsonElement("windowStart")]
    public DateTime WindowStart { get; set; }

    [BsonElement("windowEnd")]
    public DateTime WindowEnd { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
namespace WeatherApi.Domain.Enums;

public enum UserRole
{
    Public = 0,
    User = 1,
    Premium = 2,
    Admin = 3
}

