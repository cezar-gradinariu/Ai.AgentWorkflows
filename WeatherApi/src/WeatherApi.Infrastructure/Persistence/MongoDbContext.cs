using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WeatherApi.Domain.Entities;

namespace WeatherApi.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);

        ConfigureIndexes(settings.Value);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>(nameof(Users));
    public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>(nameof(RefreshTokens));
    public IMongoCollection<RateLimitCounter> RateLimitCounters => _database.GetCollection<RateLimitCounter>(nameof(RateLimitCounters));

    private void ConfigureIndexes(MongoDbSettings settings)
    {
        // User indexes
        var userIndexKeys = Builders<User>.IndexKeys.Ascending(u => u.Username);
        var userIndexModel = new CreateIndexModel<User>(userIndexKeys, new CreateIndexOptions { Unique = true });
        Users.Indexes.CreateOne(userIndexModel);

        var emailIndexKeys = Builders<User>.IndexKeys.Ascending(u => u.Email);
        var emailIndexModel = new CreateIndexModel<User>(emailIndexKeys, new CreateIndexOptions { Unique = true });
        Users.Indexes.CreateOne(emailIndexModel);

        // RefreshToken indexes
        var tokenIndexKeys = Builders<RefreshToken>.IndexKeys.Ascending(t => t.Token);
        var tokenIndexModel = new CreateIndexModel<RefreshToken>(tokenIndexKeys, new CreateIndexOptions { Unique = true });
        RefreshTokens.Indexes.CreateOne(tokenIndexModel);

        var expiresAtIndexKeys = Builders<RefreshToken>.IndexKeys.Ascending(t => t.ExpiresAt);
        var expiresAtIndexModel = new CreateIndexModel<RefreshToken>(expiresAtIndexKeys, new CreateIndexOptions { ExpireAfter = TimeSpan.Zero });
        RefreshTokens.Indexes.CreateOne(expiresAtIndexModel);

        // RateLimitCounter indexes
        var rateLimitIndexKeys = Builders<RateLimitCounter>.IndexKeys
            .Ascending(r => r.UserId)
            .Ascending(r => r.WindowStart);
        var rateLimitIndexModel = new CreateIndexModel<RateLimitCounter>(rateLimitIndexKeys);
        RateLimitCounters.Indexes.CreateOne(rateLimitIndexModel);

        var windowEndIndexKeys = Builders<RateLimitCounter>.IndexKeys.Ascending(r => r.WindowEnd);
        var windowEndIndexModel = new CreateIndexModel<RateLimitCounter>(windowEndIndexKeys, new CreateIndexOptions { ExpireAfter = TimeSpan.Zero });
        RateLimitCounters.Indexes.CreateOne(windowEndIndexModel);
    }
}

