using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;

namespace WeatherApi.Infrastructure.Repositories;

/// <summary>
/// MongoDB implementation of the weather repository.
/// </summary>
public class MongoWeatherRepository : IWeatherRepository
{
    private readonly IMongoCollection<WeatherDataPoint> _collection;
    private readonly ILogger<MongoWeatherRepository> _logger;

    public MongoWeatherRepository(IMongoDatabase database, ILogger<MongoWeatherRepository> logger)
    {
        _collection = database.GetCollection<WeatherDataPoint>("weatherdatapoints");
        _logger = logger;
        
        // Create compound index on city and timestamp for efficient queries (fixes HIGH-003)
        var indexKeys = Builders<WeatherDataPoint>.IndexKeys
            .Ascending(x => x.City)
            .Ascending(x => x.Timestamp);
        
        var indexModel = new CreateIndexModel<WeatherDataPoint>(indexKeys);
        
        // Create index with proper logging
        _collection.Indexes.CreateOneAsync(indexModel).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                _logger.LogWarning(task.Exception, 
                    "Failed to create index on weatherdatapoints collection. Index may already exist.");
            }
            else
            {
                _logger.LogInformation("Successfully created index on weatherdatapoints collection");
            }
        });
    }

    public async Task<List<WeatherDataPoint>> GetWeatherAsync(string city, DateTime from, DateTime to)
    {
        var filter = Builders<WeatherDataPoint>.Filter.Eq(x => x.City, city) &
                     Builders<WeatherDataPoint>.Filter.Gte(x => x.Timestamp, from) &
                     Builders<WeatherDataPoint>.Filter.Lte(x => x.Timestamp, to);

        return await _collection.Find(filter)
            .SortBy(x => x.Timestamp)
            .ToListAsync();
    }

    public async Task UpsertWeatherDataPointsAsync(IEnumerable<WeatherDataPoint> dataPoints)
    {
        // Use MongoDB BulkWrite API for better performance (addresses Performance Concern #1)
        var bulkOps = dataPoints.Select(dp =>
        {
            var filter = Builders<WeatherDataPoint>.Filter.Eq(x => x.City, dp.City) &
                         Builders<WeatherDataPoint>.Filter.Eq(x => x.Timestamp, dp.Timestamp);

            // Only replace if the document doesn't exist OR if the new data is newer
            var updateFilter = filter & (
                Builders<WeatherDataPoint>.Filter.Exists("LastUpdated", false) |
                Builders<WeatherDataPoint>.Filter.Lt(x => x.LastUpdated, dp.LastUpdated)
            );

            return new ReplaceOneModel<WeatherDataPoint>(updateFilter, dp)
            {
                IsUpsert = true
            };
        }).ToList();

        if (bulkOps.Any())
        {
            await _collection.BulkWriteAsync(bulkOps);
        }
    }

    public async Task UpsertWeatherDataPointAsync(WeatherDataPoint dataPoint)
    {
        // Use atomic FindOneAndReplace to avoid race conditions (fixes Issue #1)
        var filter = Builders<WeatherDataPoint>.Filter.Eq(x => x.City, dataPoint.City) &
                     Builders<WeatherDataPoint>.Filter.Eq(x => x.Timestamp, dataPoint.Timestamp);

        // Only replace if the document doesn't exist OR if the new data is newer
        var updateFilter = filter & (
            Builders<WeatherDataPoint>.Filter.Exists("LastUpdated", false) |
            Builders<WeatherDataPoint>.Filter.Lt(x => x.LastUpdated, dataPoint.LastUpdated)
        );

        var options = new FindOneAndReplaceOptions<WeatherDataPoint>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        await _collection.FindOneAndReplaceAsync(updateFilter, dataPoint, options);
    }
}
