namespace WeatherApi.Infrastructure.Persistence;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "WeatherApiDb";
    public string UsersCollectionName { get; set; } = "Users";
    public string RefreshTokensCollectionName { get; set; } = "RefreshTokens";
    public string RateLimitCountersCollectionName { get; set; } = "RateLimitCounters";
}